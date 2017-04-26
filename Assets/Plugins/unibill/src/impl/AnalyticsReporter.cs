using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniject;

namespace Unibill.Impl
{
	public class AnalyticsReporter
	{
        private const string ANALYTICS_URL = "http://stats.unibiller.com/stats";
		private const string USER_ID_KEY = "com.outlinegames.unilytics.analytics.userId";
        public const string UNIBILL_VERSION = "1.7.20";

		private UnibillConfiguration config;
		private IHTTPClient client;
        private IUtil util;
        private string userId;
		private bool restoreInProgress;
        private string levelName;
        private DateTime levelLoadTime;

		private enum EventType
		{
			purchase_succeeded,
			purchase_cancelled,
			purchase_failed,
            purchase_refunded,
            new_installation,
            new_session,
            level_change
		}

        public AnalyticsReporter(Biller biller, UnibillConfiguration config, IHTTPClient client, IStorage storage, IUtil util, ILevelLoadListener listener) {
			this.config = config;
			this.client = client;
            this.util = util;
            this.userId = getUserId (storage);
			biller.onPurchaseComplete += onSucceeded;
			biller.onPurchaseCancelled += (PurchasableItem obj) => onEvent(EventType.purchase_cancelled, obj, null);
			biller.onPurchaseRefunded += (PurchasableItem obj) => onEvent(EventType.purchase_refunded, obj, null);
			biller.onTransactionRestoreBegin += (bool obj) =>  restoreInProgress = true;
			biller.onTransactionsRestored += (bool obj) => restoreInProgress = false;
            listener.registerListener (() => onLevelLoad ());
            onEvent (EventType.new_session, null, null);
            this.levelName = util.loadedLevelName ();
            this.levelLoadTime = DateTime.UtcNow;
		}

        private void onLevelLoad() {
            var e = getBaseRequest(EventType.level_change);
            e.Add ("levelChange", encodeLevelChange ());
            levelLoadTime = DateTime.UtcNow;
            levelName = Application.loadedLevelName;
            onEvent (e);
        }
		
		private string getUserId(IStorage storage) {
			string result = storage.GetString(USER_ID_KEY, string.Empty);
			if (string.IsNullOrEmpty(result)) {
				result = Guid.NewGuid().ToString();
				storage.SetString(USER_ID_KEY, result);
                onEvent (EventType.new_installation, null, null);
			}
			return result;
		}

		private void onSucceeded(PurchaseEvent e) {
			if (restoreInProgress) {
				// We don't send notifications of restorations.
				return;
			}
			onEvent (EventType.purchase_succeeded, e.PurchasedItem, e.Receipt);
		}

		private void onCancelled(PurchaseEvent e) {
			onEvent (EventType.purchase_cancelled, e.PurchasedItem, null);
		}

        private void onEvent(EventType e, PurchasableItem item, string receipt) {
            var request = getBaseRequest (e);
            if (null != item) {
                request.Add ("item", encodeItem (item, receipt));
            }
            onEvent (request);
        }

        private void onEvent(Dictionary<string, object> e) {
			if (!string.IsNullOrEmpty (config.UnibillAnalyticsAppId)) {
                string body = MiniJSON.jsonEncode (e);
				client.doPost (ANALYTICS_URL, new PostParameter ("payload", body));
			}
		}

        private Dictionary<string, object> encodeLevelChange() {
            var dic = new Dictionary<string, object> ();
            dic.Add ("fromLevel", levelName);
            dic.Add ("fromTime", formatTimestamp(levelLoadTime));
            dic.Add ("toLevel", util.loadedLevelName ());
            dic.Add ("toTime", formatTimestamp(DateTime.UtcNow));
            return dic;
        }

        private static string formatTimestamp(DateTime timestamp) {
            return timestamp.ToString ("s", System.Globalization.CultureInfo.InvariantCulture);
        }

        private Dictionary<string, object> getBaseRequest(EventType eventType) {
            var dic = new Dictionary<string, object> ();
            dic.Add ("appId", config.UnibillAnalyticsAppId);
            dic.Add ("userId", userId);
            dic.Add ("appSecret", config.UnibillAnalyticsAppSecret);
            dic.Add ("eventType", eventType.ToString());
            dic.Add ("platform", config.CurrentPlatform.ToString ());
            dic.Add("unibillVersion", UNIBILL_VERSION);
            dic.Add("nonce", Guid.NewGuid().ToString());
            dic.Add("deviceInfo", encodeDeviceInfo());
            dic.Add("config", encodeConfig());

            return dic;
        }

        private Dictionary<string, object> encodeConfig() {
            var dic = new Dictionary<string, object>();
            dic.Add("useAmazonSandbox", config.AmazonSandboxEnabled);
            dic.Add("samsungAppsMode", config.SamsungAppsMode.ToString());
            dic.Add("useHostedConfig", config.UseHostedConfig);
            dic.Add("useWin81Sandbox", config.UseWin8_1Sandbox);
            dic.Add("useWP8Sandbox", config.WP8SandboxEnabled);

            return dic;
        }

        private Dictionary<string, object>  encodeItem(PurchasableItem item, string receipt) {
			var dic = new Dictionary<string, object> ();
			dic.Add ("id", item.Id);
			dic.Add ("currency", item.isoCurrencySymbol);
			dic.Add ("price", item.priceInLocalCurrency.ToString ());
            dic.Add("priceString", item.localizedPriceString);
            if (null != receipt) {
                dic.Add ("receipt", receipt);
            }
			return dic;
		}

        private Dictionary<string, object> encodeDeviceInfo() {
            var dic = new Dictionary<string, object>();
            dic.Add("deviceModel", util.DeviceModel);
            dic.Add("deviceName", util.DeviceName);
            dic.Add("deviceType", util.DeviceType.ToString());
            dic.Add("os", util.OperatingSystem);
            return dic;
        }
	}
}
