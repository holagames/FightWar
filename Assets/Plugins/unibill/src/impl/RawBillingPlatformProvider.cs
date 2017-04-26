using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Uniject;
using Uniject.Impl;

namespace Unibill.Impl {
    class RawBillingPlatformProvider : IRawBillingPlatformProvider {

        private UnibillConfiguration config;
        private GameObject gameObject;
        public RawBillingPlatformProvider(UnibillConfiguration config) {
            this.config = config;
            this.gameObject = new GameObject ();
        }

        public IRawGooglePlayInterface getGooglePlay() {
            return new RawGooglePlayInterface();
        }

        public IRawAmazonAppStoreBillingInterface getAmazon() {
            return new RawAmazonAppStoreBillingInterface(config);
        }

        public IStoreKitPlugin getStorekit() {
            if (UnityEngine.Application.platform == UnityEngine.RuntimePlatform.IPhonePlayer) {
                return new StoreKitPluginImpl();
            }

            return new OSXStoreKitPluginImpl();
        }

		public IRawSamsungAppsBillingService getSamsung() {
			return new RawSamsungAppsBillingInterface ();
		}

        private ILevelLoadListener listener;
        public Uniject.ILevelLoadListener getLevelLoadListener ()
        {
            if (null == listener) {
                listener = gameObject.AddComponent<UnityLevelLoadListener> ();
            }
            return listener;
        }

        private IHTTPClient client;
        public IHTTPClient getHTTPClient (IUtil util)
        {
            if (null == client) {
                client = new HTTPClient (util);
            }
            return client;
        }
    }
}
