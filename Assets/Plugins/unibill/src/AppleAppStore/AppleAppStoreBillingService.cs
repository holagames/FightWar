//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniject;

namespace Unibill.Impl {

    /// <summary>
    /// App Store implementation of <see cref="IBillingService"/>.
    /// This class has platform specific logic to handle errors from Storekit,
    /// such as a nil product list being returned, and print helpful information.
    /// </summary>
    public class AppleAppStoreBillingService : IBillingService {

        private IBillingServiceCallback biller;
        private ProductIdRemapper remapper;
        private HashSet<PurchasableItem> products;
        private HashSet<string> productsNotReturnedByStorekit = new HashSet<string>();
        private string appReceipt;
        private ILogger logger;
        public IStoreKitPlugin storekit { get; private set; }
        public AppleAppStoreBillingService(UnibillConfiguration db, ProductIdRemapper mapper, IStoreKitPlugin storekit, ILogger logger) {
            this.storekit = storekit;
            this.remapper = mapper;
            this.logger = logger;
            storekit.initialise(this);
            products = new HashSet<PurchasableItem>(db.AllPurchasableItems);
        }

        public void initialise (IBillingServiceCallback biller) {
            this.biller = biller;
            bool available = storekit.storeKitPaymentsAvailable ();
            if (available) {
                string[] platformSpecificProductIds = remapper.getAllPlatformSpecificProductIds();
                storekit.storeKitRequestProductData (string.Join (",", platformSpecificProductIds), platformSpecificProductIds);
            } else {
                biller.logError(UnibillError.STOREKIT_BILLING_UNAVAILABLE);
                biller.onSetupComplete(false);
            }
        }

        public void purchase (string item, string developerPayload) {
            if (productsNotReturnedByStorekit.Contains (item)) {
                biller.logError(UnibillError.STOREKIT_ATTEMPTING_TO_PURCHASE_PRODUCT_NOT_RETURNED_BY_STOREKIT, item);
                biller.onPurchaseFailedEvent(item);
                return;
            }
            storekit.storeKitPurchaseProduct(item);
        }

        private bool restoreInProgress;
        public void restoreTransactions () {
            restoreInProgress = true;
            storekit.storeKitRestoreTransactions();
        }

        public void onProductListReceived (string productListString) {
            if (productListString.Length == 0) {
                biller.logError (UnibillError.STOREKIT_RETURNED_NO_PRODUCTS);
                biller.onSetupComplete (false);
                return;
            }

            Dictionary<string, object> responseObject = (Dictionary<string, object>)Unibill.Impl.MiniJSON.jsonDecode(productListString);
            this.appReceipt = responseObject.getString ("appReceipt");
            Dictionary<string, object> response = responseObject.getHash ("products");
            HashSet<PurchasableItem> productsReceived = new HashSet<PurchasableItem>();
            foreach (var identifier in response.Keys) {
                if (!remapper.canMapProductSpecificId (identifier.ToString ())) {
                    biller.logError (UnibillError.UNIBILL_UNKNOWN_PRODUCTID, identifier.ToString ());
                    continue;
                }

                var item = remapper.getPurchasableItemFromPlatformSpecificId(identifier.ToString());
                Dictionary<string, object> details = (Dictionary<string, object>)response[identifier];

                PurchasableItem.Writer.setAvailable (item, true);
                PurchasableItem.Writer.setLocalizedPrice(item, details["price"].ToString());
                PurchasableItem.Writer.setLocalizedTitle(item, details["localizedTitle"].ToString());
                PurchasableItem.Writer.setLocalizedDescription(item, details["localizedDescription"].ToString());
                if (details.ContainsKey ("isoCurrencyCode")) {
                    PurchasableItem.Writer.setISOCurrencySymbol (item, details ["isoCurrencyCode"].ToString());
                }

                if (details.ContainsKey ("priceDecimal")) {
                    PurchasableItem.Writer.setPriceInLocalCurrency (item, decimal.Parse(details ["priceDecimal"].ToString()));
                }
                productsReceived.Add(item);
            }

            HashSet<PurchasableItem> productsNotReceived = new HashSet<PurchasableItem> (products);
            productsNotReceived.ExceptWith (productsReceived);
            if (productsNotReceived.Count > 0) {
                foreach (PurchasableItem product in productsNotReceived) {
                    biller.logError(UnibillError.STOREKIT_REQUESTPRODUCTS_MISSING_PRODUCT, product.Id, remapper.mapItemIdToPlatformSpecificId(product));
                }
            }

            this.productsNotReturnedByStorekit = new HashSet<string>(productsNotReceived.Select(x => remapper.mapItemIdToPlatformSpecificId(x)));

            // Register our storekit transaction observer.
            // We must wait until we have initialised to do this.
            storekit.addTransactionObserver ();

            if (this.appReceipt != null) {
                biller.setAppReceipt (this.appReceipt);
            }

            // We should complete so long as we have at least one purchasable product.
            biller.onSetupComplete(true);
        }
        
        public void onPurchaseSucceeded(string data) {
            Dictionary<string, object> response = (Dictionary<string, object>)Unibill.Impl.MiniJSON.jsonDecode(data);
            var latestReceipt = (string)response ["receipt"];
            if (!string.IsNullOrEmpty (latestReceipt)) {
                this.appReceipt = latestReceipt;
            }
            var productId = (string)response ["productId"];
            // If we're restoring, make sure it isn't a consumable.
            if (restoreInProgress) {
                if (remapper.canMapProductSpecificId (productId)) {
                    if (remapper.getPurchasableItemFromPlatformSpecificId (productId).PurchaseType == PurchaseType.Consumable) {
                        // Silently ignore this consumable.
                        logger.Log ("Ignoring restore of consumable: " + productId);
                        return;
                    }
                }
            }
            biller.onPurchaseSucceeded(productId, appReceipt);
        }
        
        public void onPurchaseCancelled(string productId) {
            biller.onPurchaseCancelledEvent(productId);
        }
        
        public void onPurchaseFailed(string productId) {
            biller.onPurchaseFailedEvent(productId);
        }

        public void onPurchaseDeferred(string productId) {
            biller.onPurchaseDeferredEvent(productId);
        }
        
        public void onTransactionsRestoredSuccess() {
            restoreInProgress = false;
            biller.onTransactionsRestoredSuccess();
        }
        
        public void onTransactionsRestoredFail(string error) {
            restoreInProgress = false;
            biller.onTransactionsRestoredFail(error);
        }

        public void onFailedToRetrieveProductList() {
            biller.logError(UnibillError.STOREKIT_FAILED_TO_RETRIEVE_PRODUCT_DATA);
            biller.onSetupComplete(true); // We should still be able to buy things, assuming they are correctly setup.
        }

        public bool hasReceipt (string forItem)
        {
            return !string.IsNullOrEmpty(appReceipt);
        }

        public string getReceipt (string forItem)
        {
            return appReceipt;
        }
    }
}
