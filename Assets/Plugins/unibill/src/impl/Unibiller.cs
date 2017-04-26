//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using Unibill;
using Unibill.Impl;
using Uniject;
using Uniject.Impl;
using UnityEngine;

public enum UnibillState {
    SUCCESS,
    SUCCESS_WITH_ERRORS,
    CRITICAL_ERROR,
}

/// <summary>
/// The public interface for Unibill.
/// 
/// For detailed Unibill documentation see www.outlinegames.com/unibill.
/// </summary>
public class Unibiller {
    private static Biller biller;
    #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
    private static DownloadManager downloadManager;
    #endif

    /// <summary>
    /// Occurs after a call to Unibiller.Initialise.
    /// <c>UnibillState.SUCCESS</c> Unibill initialised successfully.
    /// <c>UnibillState.SUCCESS_WITH_ERRORS</c> Unibill initialised successfully but with one or more non critical errors. Check the console logs or <c>Unibiller.Errors</c>.
    /// <c>UnibillState.CRITICAL_ERROR</c> Unibill encountered a critical error and cannot make purchases. Check the console logs or <c>Unibiller.Errors</c>.
    public static event Action<UnibillState> onBillerReady;

    /// <summary>
    /// Occurs when the user chose to cancel the specified purchase rather than buy it.
    /// </summary>
    public static event Action<PurchasableItem> onPurchaseCancelled;

	/// <summary>
	/// Occurs when an item is purchased.
	/// The PurchaseEvent contains both the item purchased and its purchase receipt.
	/// </summary>
	public static event Action<PurchaseEvent> onPurchaseCompleteEvent;

    /// <summary>
	/// DEPRECATED - use onPurchaseCompleteEvent.
    /// Occurs when the specified item was purchases successfully.
    /// </summary>
    public static event Action<PurchasableItem> onPurchaseComplete;

    /// <summary>
    /// Occurs when the specified purchase failed.
    /// </summary>
    public static event Action<PurchasableItem> onPurchaseFailed;

    /// <summary>
    /// iOS Specific.
    /// Occurs when parental controls are enabled and a request to purchase is sent to a parent for approval.
    /// If the purchase is approved, the onPurchaseComplete event will fire at some later point.
    /// Otherwise the onPurchaseFailed event will fire at some later point.
    /// </summary>
    public static event Action<PurchasableItem> onPurchaseDeferred;

    /// <summary>
    /// Occurs when the specified purchase was refunded.
    /// Unibill will automatically update the transaction database for the <c>PurchasableItem</c> by
    /// decrementing the purchase count.
    /// </summary>
    public static event Action<PurchasableItem> onPurchaseRefunded;

#pragma warning disable 67

    #if !UNITY_METRO
    /// <summary>
    /// Occurs when the specified Downloadable Content finishes downloading.
    /// The <c>DirectoryInfo</c> may be browsed using standard IO functions.
    /// </summary>
    public static event Action<string, DirectoryInfo> onDownloadCompletedEvent;
    #endif

    /// <summary>
    /// Occurs when the specified Downloadable Content finishes downloading.
    /// The path is provided in string form for platforms that do not define DirectoryInfo.
    /// </summary>
    public static event Action<string, string> onDownloadCompletedEventString;

    /// <summary>
    /// Occurs when a Download progresses.
    /// 
    /// The int parameter ranges from 0-99%, since
    /// the <c>onDownloadCompletedEvent</c> fires once the content is verified and unpacked.
    /// </summary>
    public static event Action<string, int> onDownloadProgressedEvent;

    /// <summary>
    /// Occurs when a Download fails permanently.
    /// 
    /// This will ONLY occur if the user is not entitled to the content,
    /// ie. they do not have a valid receipt.
    /// 
    /// Network drops, App restarts, etc, do not cancel downloads.
    /// </summary>
    public static event Action<string, string> onDownloadFailedEvent;

    /// <summary>
    /// Occurs when a call to Unibiller.restoreTransactions completed.
    /// The parameter indicates whether the restoration was successful.
    /// </summary>
    public static event Action<bool> onTransactionsRestored;

    #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
    private static DownloadManager DownloadManager;
    #endif

	/// <summary>
	/// Gets the specific billing platform in use; Google Play, Amazon, Storekit etc.
	/// </summary>
	/// <value>The billing platform.</value>
	public static BillingPlatform BillingPlatform {
		get {
			if (null != biller) {
				return biller.InventoryDatabase.CurrentPlatform;
			}
			return BillingPlatform.UnityEditor;
		}
	}
	
	/// <summary>
	/// Is Unibill initialised and ready to make purchases.
	/// </summary>
	public static bool Initialised {	
		get {
			if (null != biller) {
				return biller.State == BillerState.INITIALISED ||
					biller.State == BillerState.INITIALISED_WITH_ERROR;
			}
			return false;
		}
	}

    /// <summary>
    /// Initialise Unibill.
    /// Before calling this method, ensure you have subscribed to onBillerReady to
    /// ensure you receive Unibill's initialisation response.
    /// This method should be called once as soon as your Application launches.
    /// 
    /// runtimeProducts is an optional parameter that allows you to tell Unibill about additional
    /// products to those defined in your inventory.
    /// 
    /// You can opt to use runtimeProducts exclusively if you choose.
    /// </summary>
    public static void Initialise (List<ProductDefinition> runtimeProducts = null) {
        if (Unibiller.biller == null) {
            var mgr = new RemoteConfigManager(new UnityResourceLoader(), new UnityPlayerPrefsStorage(), new UnityLogger(), UnityEngine.Application.platform, runtimeProducts);
            var config = mgr.Config;
            var o = new GameObject ();
            UnityEngine.Object.DontDestroyOnLoad (o);
            var util = o.AddComponent<UnityUtil> ();
            var factory = new BillerFactory (new UnityResourceLoader (), new UnityLogger (), new UnityPlayerPrefsStorage (), new RawBillingPlatformProvider (config), config, util);
            Unibiller.biller = factory.instantiate ();
            _internal_hook_events(Unibiller.biller, factory);
        }
        biller.Initialise();
    }

    /// <summary>
    /// Retrieve a list of all UnibillErrors that have occured.
    /// You can find documentation for each of these errors at
    /// www.outlinegames.com/unibillerrors.
    /// </summary>
    public static UnibillError[] Errors {
        get {
            if (null != biller) {
                return biller.Errors.ToArray();
            }

            return new UnibillError[0];
        }
    }

    /// <summary>
    /// Get all PurchasableItem that can be purchased including Consumable,
    /// Non-Consumable and Subscriptions.
    /// </summary>
    public static PurchasableItem[] AllPurchasableItems {
        get { return biller.InventoryDatabase.AllPurchasableItems.ToArray(); }
    }

    /// <summary>
    /// Get all PurchasableItem of type Non-Consumable.
    /// </summary>
    public static PurchasableItem[] AllNonConsumablePurchasableItems {
        get { return biller.InventoryDatabase.AllNonConsumablePurchasableItems.ToArray(); }
    }

    /// <summary>
    /// Get all PurchasableItem of type Consumable.
    /// </summary>
    public static PurchasableItem[] AllConsumablePurchasableItems {
        get { return biller.InventoryDatabase.AllConsumablePurchasableItems.ToArray (); }
    }

    /// <summary>
    /// Get all PurchasableItem of type Subscription.
    /// </summary>
    public static PurchasableItem[] AllSubscriptions {
        get { return biller.InventoryDatabase.AllSubscriptions.ToArray (); }
    }

	/// <summary>
	/// Get the identifiers of all currencies managed by Unibill.
	/// </summary>
	public static string[] AllCurrencies {
		get { return biller.CurrencyIdentifiers; }
	}

    /// <summary>
    /// Get the specified PurchasableItem by its Unibill identifier,
    /// eg "com.companyname.100goldcoins".
    /// 
    /// Unibill identifiers must be globally unique and can refer to Consumable,
    /// Non-Consumable and Subscription purchasable items.
    /// </summary>
    public static PurchasableItem GetPurchasableItemById(string unibillPurchasableId) {
        if (null != biller) {
            return biller.InventoryDatabase.getItemById (unibillPurchasableId);
        }

        return null;
    }

    /// <summary>
    /// Initiate purchasing of the specified PurchasableItem.
    /// </summary>
    /// <param name="developerPayload">
    /// Optional and relevant only to Google Play.
    /// </param>
    public static void initiatePurchase (PurchasableItem purchasable, string developerPayload = "") {
        if (null != biller) {
            biller.purchase(purchasable, developerPayload);
        }
    }

    /// <summary>
    /// Initiate purchasing of the PurchasableItem with specified Id.
    /// </summary>
    /// <param name="developerPayload">
    /// Optional and relevant only to Google Play.
    /// </param>
    public static void initiatePurchase (string purchasableId, string developerPayload = "") {
        if (null != biller) {
            biller.purchase(purchasableId, developerPayload);
        }
    }

    /// <summary>
    /// Get the number of times this PurchasableItem has been purchased.
    /// Returns 0 for unpurchased items, a maximum of 1 for Non-Consumable items.
    /// </summary>
    public static int GetPurchaseCount (PurchasableItem item) {
        if (null != biller) {
            return biller.getPurchaseHistory(item);
        }
        return 0;
    }

    /// <summary>
    /// Get the number of times the PurchasableItem with specified Id has been purchased.
    /// Returns 0 for unpurchased items, a maximum of 1 for Non-Consumable items.
    /// </summary>
    public static int GetPurchaseCount (string purchasableId) {
        if (null != biller) {
            return biller.getPurchaseHistory(purchasableId);
        }
        return 0;
    }

	/// <summary>
	/// Gets the balance for the specified Unibill managed currency.
	/// </summary>
	/// <returns>The currency balance or 0 if the currency is unknown.</returns>
	public static decimal GetCurrencyBalance(string currencyIdentifier) {
        if (null != biller) {
            return biller.getCurrencyBalance (currencyIdentifier);
        }
        return 0;
	}

	/// <summary>
	/// Credits the specified currency by the specified amount.
	/// </summary>
	public static void CreditBalance(string currencyIdentifier, decimal amount) {
        if (null != biller) {
            biller.creditCurrencyBalance (currencyIdentifier, amount);
        }
	}

	/// <summary>
	/// Debits the balance.
	/// </summary>
	/// <returns><c>true</c>, if balance was debited (sufficient funds were available), <c>false</c> otherwise.</returns>
	public static bool DebitBalance(string currencyIdentifier, decimal amount) {
        if (null != biller) {
            return biller.debitCurrencyBalance (currencyIdentifier, amount);
        }
        return false;
	}

    /// <summary>
    /// Initiate a restore of all purchased items the user has previously purchased,
    /// whether on this device or another.
    /// 
    /// This should be called the first time your Application is run on any given device.
    /// You should also provide a way for users to manually reset their purchases if they so choose.
    /// 
    /// Note that Consumable purchases cannot and will not be restored using this method; neither Amazon,
    /// Google or Apple track Consumables.
    /// 
    /// <c>onPurchaseComplete</c> will be fired for each PurchasableItem the user has previously purchases.
    /// Unibill will also update its own transaction database to reflect the purchases.
    /// </summary>
    public static void restoreTransactions () {
        if (null != biller) {
            biller.restoreTransactions();
        }
    }

    /// <summary>
    /// Clears Unibill's local purchase database.
    /// Warning! This will erase Unibill's record of all purchases made!
    /// </summary>
    public static void clearTransactions() {
        if (null != biller) {
            biller.ClearPurchases ();
        }
    }

    /// <summary>
    /// Downloads the content.
    /// </summary>
    /// <param name="bundleId">Bundle identifier.</param>
    /// <param name="proofOfPurchase">Proof of purchase.</param>
    public static void DownloadContent(string bundleId, PurchasableItem proofOfPurchase = null) {
        #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
        if (null != downloadManager) {
            string receipt = string.Empty;
            if (null != proofOfPurchase) {
                if (GetPurchaseCount(proofOfPurchase) == 0) {
                    if (null != onDownloadFailedEvent) {
                        onDownloadFailedEvent(bundleId, "Proof of purchase is not owned!");
                        return;
                    }
                }
                receipt = proofOfPurchase.receipt;
            }
            downloadManager.downloadContentFor (bundleId, receipt);
        }
        #endif
    }

    #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
    /// <summary>
    /// Get the downloaded content for a <c>PurchasableItem</c>.
    /// The content must have downloaded successfully,
    /// which can be checked with a call to <c>IsContentDownloaded</c>.
    /// </summary>
    /// <returns>The Directory containing the content, or NULL.</returns>
    public static DirectoryInfo GetDownloadableContentFor(PurchasableItem item) {
        if (null != downloadManager && item.hasDownloadableContent) {
            return new DirectoryInfo(downloadManager.getContentPath (item.downloadableContentId));
        }

        return null;
    }
    #endif

    /// <summary>
    /// Get the downloaded content for a <c>PurchasableItem</c>.
    /// The content must have downloaded successfully,
    /// which can be checked with a call to <c>IsContentDownloaded</c>.
    /// </summary>
    /// <returns>The Directory path containing the content, or NULL.</returns>
    public static string GetDownloadableContentPathFor(string bundleId) {
        #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
        if (null != downloadManager) {
            return downloadManager.getContentPath(bundleId);
        }
        #endif
        return null;
    }

    /// <summary>
    /// Determine if content is downloaded and available for the specified item.
    /// </summary>
    public static bool IsContentDownloaded(string bundleId) {
        #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
        if (null != downloadManager) {
            return downloadManager.isDownloaded(bundleId);
        }
        #endif
        return false;
    }

    /// <summary>
    /// Determine if content is scheduled for download.
    /// Returns false once content has been successfully downloaded.
    /// </summary>
    public static bool IsDownloadScheduled(string bundleId) {
        #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
        if (null != downloadManager) {
            return downloadManager.isDownloadScheduled(bundleId);
        }
        #endif
        return false;
    }

    /// <summary>
    /// Deletes locally downloaded content for the <c>PurchasableItem</c>.
    /// </summary>
    public static void DeleteDownloadedContent(string bundleId) {
        #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
        if (null != downloadManager) {
            downloadManager.deleteContent (bundleId);
        }
        #endif
    }
    /// <summary>
    /// Access functionality specific to iOS.
    /// </summary>
    public static IAppleExtensions getAppleExtensions()
    {
        return biller.getAppleExtensions();
    }
    /// <summary>
    /// Visible only for unit testing.
    /// Do NOT call this method.
    /// </summary>
    public static void _internal_hook_events (Biller biller, BillerFactory factory) {
        biller.onBillerReady += (success) => {
            if (onBillerReady != null) {
                if (success) {
                    #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
                    // Download manager needs Unibill to be initialised to get purchase receipts.
                    downloadManager = factory.instantiateDownloadManager (biller);
                    downloadManager.onDownloadCompletedEvent += (item, path) => {
                        if (null != onDownloadCompletedEvent) {
                            onDownloadCompletedEvent(item, new DirectoryInfo(path));
                        }
                    };
                    downloadManager.onDownloadCompletedEvent += onDownloadCompletedEventString;
                    downloadManager.onDownloadFailedEvent += onDownloadFailedEvent;
                    downloadManager.onDownloadProgressedEvent += onDownloadProgressedEvent;
                    #else
                    onDownloadCompletedEventString += (x, y) => { };
                    #endif
                    onBillerReady(biller.State == Unibill.Impl.BillerState.INITIALISED ? UnibillState.SUCCESS : UnibillState.SUCCESS_WITH_ERRORS);
                } else {
                    onBillerReady(UnibillState.CRITICAL_ERROR);
                }
            }
        };

        biller.onPurchaseCancelled += _onPurchaseCancelled;
        biller.onPurchaseComplete += _onPurchaseComplete;
        biller.onPurchaseFailed += _onPurchaseFailed;
        biller.onPurchaseDeferred += _onPurchaseDeferred;
        biller.onPurchaseRefunded += _onPurchaseRefunded;
        biller.onTransactionsRestored += _onTransactionsRestored;
    }

	private static void _onPurchaseCancelled(PurchasableItem item) {
		if (null != onPurchaseCancelled) {
			onPurchaseCancelled (item);
		}
	}

	private static void _onPurchaseComplete(PurchaseEvent e) {
		if (null != onPurchaseComplete) {
			onPurchaseComplete (e.PurchasedItem);
		}

		if (null != onPurchaseCompleteEvent) {
			onPurchaseCompleteEvent (e);
		}
	}

	private static void _onPurchaseFailed(PurchasableItem item) {
		if (null != onPurchaseFailed) {
			onPurchaseFailed (item);
		}
	}

    private static void _onPurchaseDeferred(PurchasableItem item) {
        if (null != onPurchaseDeferred) {
            onPurchaseDeferred(item);
        }
    }

	private static void _onPurchaseRefunded(PurchasableItem item) {
		if (null != onPurchaseRefunded) {
			onPurchaseRefunded (item);
		}
	}

	private static void _onTransactionsRestored(bool success) {
		if (null != onTransactionsRestored) {
			onTransactionsRestored (success);
		}
	}
}
