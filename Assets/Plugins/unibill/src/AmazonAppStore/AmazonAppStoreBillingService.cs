//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using Unibill.Impl;
using Uniject;

namespace Unibill.Impl {
    public class AmazonAppStoreBillingService : IBillingService {
        #region IBillingService implementation

        private IBillingServiceCallback callback;
        private ProductIdRemapper remapper;
        private UnibillConfiguration db;
        private ILogger logger;
        private IRawAmazonAppStoreBillingInterface amazon;
        private HashSet<string> unknownAmazonProducts = new HashSet<string>();
        private TransactionDatabase tDb;

        public AmazonAppStoreBillingService(IRawAmazonAppStoreBillingInterface amazon, ProductIdRemapper remapper, UnibillConfiguration db, TransactionDatabase tDb, ILogger logger) {
            this.remapper = remapper;
            this.db = db;
            this.logger = logger;
            logger.prefix = "UnibillAmazonBillingService";
            this.amazon = amazon;
            this.tDb = tDb;
        }

        public void initialise (IBillingServiceCallback biller) {
            this.callback = biller;
            amazon.initialise(this);
            amazon.initiateItemDataRequest(remapper.getAllPlatformSpecificProductIds());
        }

        public void purchase (string item, string developerPayload) {
            if (unknownAmazonProducts.Contains (item)) {
                callback.logError(UnibillError.AMAZONAPPSTORE_ATTEMPTING_TO_PURCHASE_PRODUCT_NOT_RETURNED_BY_AMAZON, item);
                callback.onPurchaseFailedEvent(item);
                return;
            }
            amazon.initiatePurchaseRequest(item);
        }

        public void restoreTransactions () {
            amazon.restoreTransactions();
        }

        #endregion

        public void onSDKAvailable (string isSandbox) {
            bool sandbox = bool.Parse (isSandbox);
            logger.Log("Running against {0} Amazon environment", sandbox ? "SANDBOX" : "PRODUCTION");
        }

        public void onGetItemDataFailed() {
            this.callback.logError(UnibillError.AMAZONAPPSTORE_GETITEMDATAREQUEST_FAILED);
            callback.onSetupComplete(true);
        }

        public void onProductListReceived (string productListString) {
            Dictionary<string, object> responseHash = (Dictionary<string, object>)Unibill.Impl.MiniJSON.jsonDecode(productListString);
            onUserIdRetrieved (responseHash.getString ("userId"));

            Dictionary<string, object> products = responseHash.getHash ("products");
            if (products.Count == 0) {
                callback.logError (UnibillError.AMAZONAPPSTORE_GETITEMDATAREQUEST_NO_PRODUCTS_RETURNED);
                callback.onSetupComplete (false);
                return;
            }

            HashSet<PurchasableItem> productsReceived = new HashSet<PurchasableItem>();
            foreach (var identifier in products.Keys) {
                var item = remapper.getPurchasableItemFromPlatformSpecificId(identifier.ToString());
                Dictionary<string, object> details = (Dictionary<string, object>)products[identifier];

                PurchasableItem.Writer.setAvailable (item, true);
                PurchasableItem.Writer.setLocalizedPrice(item, details["price"].ToString());
                PurchasableItem.Writer.setLocalizedTitle(item, (string) details["localizedTitle"]);
                PurchasableItem.Writer.setLocalizedDescription(item, (string) details["localizedDescription"]);
                PurchasableItem.Writer.setISOCurrencySymbol(item, details.getString("isoCurrencyCode"));
                PurchasableItem.Writer.setPriceInLocalCurrency (item, decimal.Parse (details.getString("priceDecimal")));
                productsReceived.Add(item);
            }
            
            HashSet<PurchasableItem> productsNotReceived = new HashSet<PurchasableItem> (db.AllPurchasableItems);
            productsNotReceived.ExceptWith (productsReceived);
            if (productsNotReceived.Count > 0) {
                foreach (PurchasableItem product in productsNotReceived) {
                    this.unknownAmazonProducts.Add(remapper.mapItemIdToPlatformSpecificId(product));
                    callback.logError(UnibillError.AMAZONAPPSTORE_GETITEMDATAREQUEST_MISSING_PRODUCT, product.Id, remapper.mapItemIdToPlatformSpecificId(product));
                }
            }
        }

        private void onUserIdRetrieved (string userId) {
            tDb.UserId = userId;
        }

        public void onTransactionsRestored (string successString) {
            bool success = bool.Parse(successString);
            if (success) {
                callback.onTransactionsRestoredSuccess();
            } else {
                callback.onTransactionsRestoredFail(string.Empty);
            }
        }

        public void onPurchaseFailed(string item) {
            callback.onPurchaseFailedEvent(item);
        }

        public void onPurchaseCancelled(string item) {
            callback.onPurchaseCancelledEvent(item);
        }

        public void onPurchaseSucceeded(string json) {
            Dictionary<string, object> response = (Dictionary<string, object>)Unibill.Impl.MiniJSON.jsonDecode(json);

            string productId = (string) response ["productId"];
            string token = (string) response ["purchaseToken"];
            callback.onPurchaseSucceeded (productId, token);
        }

        public void onPurchaseUpdateFailed () {
            logger.LogWarning("AmazonAppStoreBillingService: onPurchaseUpdate() failed.");
        }

        private bool finishedSetup;
        public void onPurchaseUpdateSuccess (string json) {
            Dictionary<string, object> response = (Dictionary<string, object>)Unibill.Impl.MiniJSON.jsonDecode(json);

            var restored = response.get<List<object>> ("restored");
            foreach (Dictionary<string, object> restoredItem in restored) {
                callback.onPurchaseSucceeded (restoredItem.getString ("sku"), restoredItem.getString ("receipt"));
            }

            var revoked = response.get<List<object>> ("revoked");
            foreach (string revokedItem in revoked) {
                callback.onPurchaseRefundedEvent (revokedItem);
            }

            if (!finishedSetup) {
                finishedSetup = true;
                callback.onSetupComplete (true);
            }
        }

        public bool hasReceipt (string forItem)
        {
            return false;
        }

        public string getReceipt (string forItem)
        {
            throw new NotImplementedException ();
        }
    }
}
