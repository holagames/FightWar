//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Uniject;
using Unibill;
using Unibill.Impl;
using UnityEngine;


namespace Unibill.Impl {
    public class GooglePlayBillingService : IBillingService {

        private string publicKey;
        private IRawGooglePlayInterface rawInterface;
        private IBillingServiceCallback callback;
        private ProductIdRemapper remapper;
        private UnibillConfiguration db;
        private ILogger logger;
        #if UNITY_ANDROID
        private RSACryptoServiceProvider cryptoProvider;
        #endif
        private HashSet<string> unknownAmazonProducts = new HashSet<string>();

        public GooglePlayBillingService (IRawGooglePlayInterface rawInterface,
                                         UnibillConfiguration config,
                                         ProductIdRemapper remapper,
                                         ILogger logger) {
            this.rawInterface = rawInterface;
            this.publicKey = config.GooglePlayPublicKey;
            this.remapper = remapper;
            this.db = config;
            this.logger = logger;
            #if UNITY_ANDROID
            this.cryptoProvider = PEMKeyLoader.CryptoServiceProviderFromPublicKeyInfo(publicKey);
            #endif
        }

        public void initialise (IBillingServiceCallback callback) {
            this.callback = callback;
            if (null == publicKey || publicKey.Equals ("[Your key]")) {
                callback.logError (UnibillError.GOOGLEPLAY_PUBLICKEY_NOTCONFIGURED, publicKey);
                callback.onSetupComplete (false);
                return;
            }

            var encoder = new Dictionary<string, object>();
            encoder.Add ("publicKey", this.publicKey);
            var productIds = new List<string>();
            List<object> products = new List<object>();
            foreach (var item in db.AllPurchasableItems) {
                Dictionary<string, object> product = new Dictionary<string, object>();
                var id = remapper.mapItemIdToPlatformSpecificId(item);
                productIds.Add(id);
                product.Add ("productId", id);
                product.Add ("consumable", item.PurchaseType == PurchaseType.Consumable);
                products.Add (product);
            }
            encoder.Add("products", products);

            var json = encoder.toJson();
            rawInterface.initialise(this, json, productIds.ToArray());
        }

        public void restoreTransactions () {
            rawInterface.restoreTransactions();
        }

        public void purchase (string item, string developerPayload) {
            if (unknownAmazonProducts.Contains (item)) {
                callback.logError(UnibillError.GOOGLEPLAY_ATTEMPTING_TO_PURCHASE_PRODUCT_NOT_RETURNED_BY_GOOGLEPLAY, item);
                callback.onPurchaseFailedEvent(item);
                return;
            }

            var args = new Dictionary<string, object>();
            args ["productId"] = item;
            args ["developerPayload"] = developerPayload;

            rawInterface.purchase(MiniJSON.jsonEncode(args));
        }


        // Callbacks
        public void onBillingNotSupported() {
            callback.logError(UnibillError.GOOGLEPLAY_BILLING_UNAVAILABLE);
            callback.onSetupComplete(false);
        }

        public void onPurchaseSucceeded(string json) {
            Dictionary<string, object> response = (Dictionary<string, object>)Unibill.Impl.MiniJSON.jsonDecode(json);
            var signature = (string) response ["signature"];
            var productId = (string) response ["productId"];
            if (!verifyReceipt (signature)) {
                logger.Log ("Signature is invalid!");
                onPurchaseFailed (productId);
                return;
            }
            callback.onPurchaseSucceeded (productId, signature);
        }

        public void onPurchaseCancelled (string item) {
            callback.onPurchaseCancelledEvent(item);
        }

        public void onPurchaseRefunded(string item) {
            callback.onPurchaseRefundedEvent(item);
        }

        public void onPurchaseFailed(string item) {
            callback.onPurchaseFailedEvent(item);
        }

        public void onTransactionsRestored (string success) {
            if (bool.Parse (success)) {
                callback.onTransactionsRestoredSuccess ();
            } else {
                callback.onTransactionsRestoredFail("");
            }
        }

        public void onInvalidPublicKey(string key) {
            callback.logError(UnibillError.GOOGLEPLAY_PUBLICKEY_INVALID, key);
            callback.onSetupComplete(false);
        }

        public void onProductListReceived (string productListString) {

            Dictionary<string, object> response = (Dictionary<string, object>)Unibill.Impl.MiniJSON.jsonDecode(productListString);

            if (response.Count == 0) {
                callback.logError (UnibillError.GOOGLEPLAY_NO_PRODUCTS_RETURNED);
                callback.onSetupComplete (false);
                return;
            }

            HashSet<PurchasableItem> productsReceived = new HashSet<PurchasableItem>();
            foreach (var identifier in response.Keys) {
                if (remapper.canMapProductSpecificId(identifier.ToString())) {
                    var item = remapper.getPurchasableItemFromPlatformSpecificId(identifier.ToString());
                    Dictionary<string, object> details = (Dictionary<string, object>)response[identifier];

                    PurchasableItem.Writer.setAvailable (item, true);
                    PurchasableItem.Writer.setLocalizedPrice(item,  details["price"].ToString());
                    PurchasableItem.Writer.setLocalizedTitle(item, (string) details["localizedTitle"]);
                    PurchasableItem.Writer.setLocalizedDescription(item, (string) details["localizedDescription"]);
					PurchasableItem.Writer.setISOCurrencySymbol(item, details.getString("isoCurrencyCode"));
					long priceInMicros = details.getLong("priceInMicros");

					decimal price = new decimal (priceInMicros) / 1000000m;
					PurchasableItem.Writer.setPriceInLocalCurrency(item, price);
                    productsReceived.Add(item);
                } else {
                    logger.LogError("Warning: Unknown product identifier: {0}", identifier.ToString());
                }
            }

            HashSet<PurchasableItem> productsNotReceived = new HashSet<PurchasableItem> (db.AllPurchasableItems);
            productsNotReceived.ExceptWith (productsReceived);
            if (productsNotReceived.Count > 0) {
                foreach (PurchasableItem product in productsNotReceived) {
                    this.unknownAmazonProducts.Add(remapper.mapItemIdToPlatformSpecificId(product));
                    callback.logError(UnibillError.GOOGLEPLAY_MISSING_PRODUCT, product.Id, remapper.mapItemIdToPlatformSpecificId(product));
                }
            }
			
			logger.Log("Received product list, polling for consumables...");
			rawInterface.pollForConsumables();
        }
		
		public void onPollForConsumablesFinished(string json) {
			logger.Log("Finished poll for consumables, completing init.");
			Dictionary<string, object> response = (Dictionary<string, object>)Unibill.Impl.MiniJSON.jsonDecode(json);
			if (null != response) {
				var ownedSubscriptions = response.getStringList("ownedSubscriptions");
				if (null != ownedSubscriptions) {
					callback.onActiveSubscriptionsRetrieved (ownedSubscriptions);
				}

                var ownedItems = response.getHash ("ownedItems");
                if (null != ownedItems) {
                    foreach (var item in ownedItems) {
                        callback.onPurchaseReceiptRetrieved (item.Key, item.Value.ToString());
                    }
                }
			}
	        callback.onSetupComplete(true);
		}

        public bool hasReceipt (string forItem)
        {
            return false;
        }

        public string getReceipt (string forItem)
        {
            throw new NotImplementedException ();
        }

        private bool verifyReceipt(string receipt) {
            #if UNITY_ANDROID
            try {
                var fields = (Dictionary<string, object>) MiniJSON.jsonDecode (receipt);
                if (null == fields) {
                    return false;
                }

                var base64Signature = fields.getString ("signature");
                var json = fields.getString ("json");

                if (null == base64Signature || null == json) {
                    return false;
                }

                byte[] signature = Convert.FromBase64String(base64Signature);
                SHA1Managed sha = new SHA1Managed();
                byte[] data = Encoding.UTF8.GetBytes(json);

                return cryptoProvider.VerifyData(data, sha, signature);
            } catch (Exception e) {
                logger.Log ("Validation exception");
                logger.Log (e.Message);
                logger.Log (e.StackTrace.ToString ());
                return false;
            }
            #else
            return true;
            #endif
        }
    }
}
	