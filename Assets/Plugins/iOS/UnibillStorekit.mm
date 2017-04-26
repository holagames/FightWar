#import "UnibillStorekit.h"

@implementation EBPurchase

#define UNITY_GAMEOBJECT_NAME "AppleAppStoreCallbackMonoBehaviour"

NSString* cachedReceipt;

-(NSString*) getAppReceipt {
    
    NSURL *receiptURL = [[NSBundle mainBundle] appStoreReceiptURL];
    if ([[NSFileManager defaultManager] fileExistsAtPath:[receiptURL path]]) {
        NSData *receipt = [NSData dataWithContentsOfURL:receiptURL];
        NSString* result = [receipt base64EncodedStringWithOptions:0];
        if (NULL == cachedReceipt) {
            cachedReceipt = result;
#if !__has_feature(objc_arc)
            [cachedReceipt retain];
#endif
            return result;
        }
        if ([cachedReceipt isEqualToString:result]) {
            // No new receipt data to push to Unity.
            return @"";
        } else {
#if !__has_feature(objc_arc)
            [cachedReceipt release];
#endif
            cachedReceipt = result;
#if !__has_feature(objc_arc)
            [cachedReceipt retain];
#endif
            return result;
        }
    }
    
    NSLog(@"Unibill: No App Receipt found!");
    return @"";
}


-(NSString*) selectReceipt:(SKPaymentTransaction*) transaction {
    
    float version = [[[UIDevice currentDevice] systemVersion] floatValue];
    if (version < 7) {
        if (nil == transaction) {
            return @"";
        }
        NSString* receipt;
        receipt = [[NSString alloc] initWithData:transaction.transactionReceipt encoding: NSUTF8StringEncoding];
        
        return receipt;
    } else {
        return [self getAppReceipt];
    }
}

-(void) pollRequestProductData {
    
    self.retrievedProductData = false;
    [self.requestCondition lock];
    while (!self.retrievedProductData) {
        self.receivedRequestProductsResponse = false;
        
        NSLog(@"Unibill: Attempting to fetch Storekit product data...");
        // Initiate a product request of the Product ID.
        self.request = [[SKProductsRequest alloc] initWithProductIdentifiers:productIds];
        self.request.delegate = self;
        [self.request start];
        
        // Wait for signal.
        while (!self.receivedRequestProductsResponse) {
            [self.requestCondition wait];
        }
        
        [NSThread sleepForTimeInterval:2];
    }
    
    [self.requestCondition unlock];
}


-(bool) requestProducts:(NSSet*)paramIds
{
    productIds = [[NSSet alloc] initWithSet:paramIds];
    if (productIds != nil) {
        
        NSLog(@"Unibill: requestProducts:%@", productIds);
        if ([SKPaymentQueue canMakePayments]) {
            // Yes, In-App Purchase is enabled on this device.
            // Proceed to fetch available In-App Purchase items.
            NSThread *mythread = [[NSThread alloc] initWithTarget:self selector:@selector(pollRequestProductData) object:nil];
            [mythread start];
            
            return YES;
            
        } else {
            return NO;
        }
        
    } else {
        return NO;
    }
}

-(bool) purchaseProduct:(NSString*)requestedProductId
{
    SKProduct* requestedProduct = nil;
    for (SKProduct* product in validProducts) {
        if ([product.productIdentifier isEqualToString:requestedProductId]) {
            requestedProduct = product;
            break;
        }
    }
    
    if (requestedProduct != nil) {
        
        NSLog(@"Unibill purchaseProduct: %@", requestedProduct.productIdentifier);
        
        if ([SKPaymentQueue canMakePayments]) {
            SKPayment *paymentRequest = [SKPayment paymentWithProduct:requestedProduct];
            [[SKPaymentQueue defaultQueue] addPayment:paymentRequest];
            
            return YES;
            
        } else {
            NSLog(@"Unibill purchaseProduct: IAP Disabled");
            
            return NO;
        }
        
    } else {
        NSString* message = [NSString stringWithFormat:@"Unknown product identifier:%@", requestedProductId];
        UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onProductPurchaseFailed", message.UTF8String);
        return YES;
    }
}

-(bool) restorePurchase
{
    NSLog(@"Unibill restorePurchase");
    
    if ([SKPaymentQueue canMakePayments]) {
        // Request to restore previous purchases.
        [[SKPaymentQueue defaultQueue] restoreCompletedTransactions];
        
        return YES;
        
    } else {
        // Notify user that In-App Purchase is Disabled.
        return NO;
    }
}

-(void) addTransactionObserver {
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
}

#pragma mark -
#pragma mark SKProductsRequestDelegate Methods

// Store Kit returns a response from an SKProductsRequest.
- (void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response {
    
    [self.requestCondition lock];
    
    // Parse the received product info.
    //self.validProduct = nil;
    self.retrievedProductData = true;
    int count = [response.products count];
    if (count>0) {
        NSLog(@"Unibill: productsRequest:didReceiveResponse:%@", response.products);
        // Record our products.
        validProducts = [[NSArray alloc] initWithArray:response.products];
        NSMutableDictionary* dic = [[NSMutableDictionary alloc] init];
        
        NSMutableDictionary* products = [[NSMutableDictionary alloc] init];
        [dic setObject:products forKey:@"products"];
        [dic setObject:[self selectReceipt:nil]  forKey:@"appReceipt"];
        
        for (SKProduct* product in validProducts) {
            NSMutableDictionary* entry = [[NSMutableDictionary alloc] init];
            
            NSNumberFormatter *numberFormatter = [[NSNumberFormatter alloc] init];
            [numberFormatter setFormatterBehavior:NSNumberFormatterBehavior10_4];
            [numberFormatter setNumberStyle:NSNumberFormatterCurrencyStyle];
            [numberFormatter setLocale:product.priceLocale];
            NSString *formattedString = [numberFormatter stringFromNumber:product.price];
#if !__has_feature(objc_arc)
            [numberFormatter release];
#endif
            
            if (NULL != product.price) {
                [entry setObject:product.price forKey:@"priceDecimal"];
            }
            
            if (NULL != product.priceLocale) {
                NSString *currencyCode = [product.priceLocale objectForKey:NSLocaleCurrencyCode];
                [entry setObject:currencyCode forKey:@"isoCurrencyCode"];
            }
            
            if (NULL == product.productIdentifier) {
                NSLog(@"Unibill: Product is missing an identifier!");
                continue;
            }
            
            if (NULL == formattedString) {
                NSLog(@"Unibill: Unable to format a localized price");
                [entry setObject:@"" forKey:@"price"];
            } else {
                [entry setObject:formattedString forKey:@"price"];
            }
            if (NULL == product.localizedTitle) {
                NSLog(@"Unibill: no localized title for: %@. Have your products been disapproved in itunes connect?", product.productIdentifier);
                [entry setObject:@"" forKey:@"localizedTitle"];
            } else {
                [entry setObject:product.localizedTitle forKey:@"localizedTitle"];
            }
            
            if (NULL == product.localizedDescription) {
                NSLog(@"Unibill: no localized description for: %@. Have your products been disapproved in itunes connect?", product.productIdentifier);
                [entry setObject:@"" forKey:@"localizedDescription"];
            } else {
                [entry setObject:product.localizedDescription forKey:@"localizedDescription"];
            }
            
            [products setObject:entry forKey:product.productIdentifier];
        }
        
        NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:0 error:nil];
        NSString* result = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onProductListReceived", [result UTF8String]);
#if !__has_feature(objc_arc)
        [result release];
#endif
    } else {
        if (0 == [response.invalidProductIdentifiers count]) {
            // It seems we got no response at all.
            self.retrievedProductData = false;
        } else {
            // Call back to Unity - fail
            UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onProductListReceived", "");
        }
    }
    
    // Send signal.
    self.receivedRequestProductsResponse = true;
    [self.requestCondition signal];
    [self.requestCondition unlock];
}


#pragma mark -
#pragma mark SKPaymentTransactionObserver Methods

- (void)request:(SKRequest *)request didFailWithError:(NSError *)error {
    NSLog(@"Unibill: SKProductRequest::didFailWithError: %i, %@", error.code, error.description);
    [self.requestCondition lock];
    self.retrievedProductData = false;
    
    // Send signal to our polling loop.
    self.receivedRequestProductsResponse = true;
    [self.requestCondition signal];
    [self.requestCondition unlock];
}

- (void)requestDidFinish:(SKRequest *)request {
    self.request = nil;
}

// The transaction status of the SKPaymentQueue is sent here.
- (void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions {
    NSLog(@"Unibill: updatedTransactions");
    for(SKPaymentTransaction *transaction in transactions) {
        switch (transaction.transactionState) {
                
            case SKPaymentTransactionStatePurchasing:
                // Item is still in the process of being purchased
                break;
                
            case SKPaymentTransactionStatePurchased:
            case SKPaymentTransactionStateRestored: {
                // Item was successfully purchased or restored.
                NSMutableDictionary* dic;
                dic = [[NSMutableDictionary alloc] init];
                [dic setObject:transaction.payment.productIdentifier forKey:@"productId"];
                
                [dic setObject:[self selectReceipt:transaction]  forKey:@"receipt"];
                
                NSData* data;
                data = [NSJSONSerialization dataWithJSONObject:dic options:0 error:nil];
                NSString* result;
                result = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
                
                UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onProductPurchaseSuccess", result.UTF8String);
#if !__has_feature(objc_arc)
                [result release];
                [dic release];
#endif
                
                // After customer has successfully received purchased content,
                // remove the finished transaction from the payment queue.
                [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
                break;
            }
            case SKPaymentTransactionStateDeferred:
                NSLog(@"Unibill: purchaseDeferred");
                UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onProductPurchaseDeferred", transaction.payment.productIdentifier.UTF8String);
                break;
            case SKPaymentTransactionStateFailed:
                // Purchase was either cancelled by user or an error occurred.
                
                NSString* errorCode = [NSString stringWithFormat:@"%d",transaction.error.code];
                if (transaction.error.code != SKErrorPaymentCancelled) {
                    NSLog(@"Unibill: purchaseFailed: %@", errorCode);
                    UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onProductPurchaseFailed", transaction.payment.productIdentifier.UTF8String);
                } else {
                    NSLog(@"Unibill: purchaseFailed: %@", errorCode);
                    UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onProductPurchaseCancelled", transaction.payment.productIdentifier.UTF8String);
                }
                
                // Finished transactions should be removed from the payment queue.
                [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
                break;
        }
    }
}

// Called when one or more transactions have been removed from the queue.
- (void)paymentQueue:(SKPaymentQueue *)queue removedTransactions:(NSArray *)transactions
{
    NSLog(@"Unibill removedTransactions");
}

// Called when SKPaymentQueue has finished sending restored transactions.
- (void)paymentQueueRestoreCompletedTransactionsFinished:(SKPaymentQueue *)queue {
    
    NSLog(@"Unibill paymentQueueRestoreCompletedTransactionsFinished");
    
    if ([queue.transactions count] == 0) {
        // Queue does not include any transactions, so either user has not yet made a purchase
        // or the user's prior purchase is unavailable, so notify app (and user) accordingly.
        
        NSLog(@"Unibill restore queue.transactions count == 0");
    } else {
        // Queue does contain one or more transactions, so return transaction data.
        // App should provide user with purchased product.
        
        for(SKPaymentTransaction *transaction in queue.transactions) {
            
            // Item was successfully purchased or restored.
            NSMutableDictionary* dic;
            dic = [[NSMutableDictionary alloc] init];
            [dic setObject:transaction.payment.productIdentifier forKey:@"productId"];
            
            [dic setObject:[self selectReceipt:transaction]  forKey:@"receipt"];
            
            NSData* data;
            data = [NSJSONSerialization dataWithJSONObject:dic options:0 error:nil];
            NSString* result;
            result = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            
            UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onProductPurchaseSuccess", result.UTF8String);
#if !__has_feature(objc_arc)
            [result release];
            [dic release];
#endif
        }
    }
    
    UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onTransactionsRestoredSuccess", "");
}

// Called if an error occurred while restoring transactions.
- (void)paymentQueue:(SKPaymentQueue *)queue restoreCompletedTransactionsFailedWithError:(NSError *)error
{
    NSLog(@"restoreCompletedTransactionsFailedWithError");
    // Restore was cancelled or an error occurred, so notify user.
    
    UnitySendMessage(UNITY_GAMEOBJECT_NAME, "onTransactionsRestoredFail", error.localizedDescription.UTF8String);
    
}


#pragma mark - Internal Methods & Events

- (id)init {
    if ( self = [super init] ) {
        validProducts = nil;
        self.requestCondition = [[NSCondition alloc] init];
    }
    return self;
}

- (void)dealloc
{
#if !__has_feature(objc_arc)
    [super dealloc];
#endif
}

@end

// Converts C style string to NSString
NSString* UnibillCreateNSString (const char* string)
{
    if (string)
        return [NSString stringWithUTF8String: string];
    else
        return [NSString stringWithUTF8String: ""];
}

EBPurchase* _instance = NULL;

EBPurchase* _getInstance() {
    if (NULL == _instance) {
        _instance = [[EBPurchase alloc] init];
    }
    return _instance;
}

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
    
    bool _storeKitPaymentsAvailable () {
        return [SKPaymentQueue canMakePayments];
    }
    
    void _storeKitRequestProductData (const char* productIdentifiers) {
        NSLog(@"Unibill: requestProductData");
        NSString* sIds = [NSString stringWithUTF8String:productIdentifiers];
        NSArray* splits = [[NSArray alloc] init];
        splits = [sIds componentsSeparatedByString:@","];
        NSSet* ids = [NSSet setWithArray:splits];
        [_getInstance() requestProducts:ids];
        
        NSLog(@"Unibill: Traceout: requestProductData");
    }
    
    void _storeKitPurchaseProduct (const char* productId) {
        NSLog(@"Unibill: _storeKitPurchaseProduct");
        NSString* str = UnibillCreateNSString(productId);
        [_getInstance() purchaseProduct:str];
        NSLog(@"Unibill: Traceout: _storeKitPurchaseProduct");
    }
    
    void _storeKitRestoreTransactions() {
        NSLog(@"Unibill: _storeKitRestoreTransactions");
        [_getInstance() restorePurchase];
        NSLog(@"Unibill: Traceout: _storeKitRestoreTransactions");
    }
    
    void _storeKitAddTransactionObserver() {
        NSLog(@"Unibill: _storeKitAddTransactionObserver");
        [_getInstance() addTransactionObserver];
    }
}

