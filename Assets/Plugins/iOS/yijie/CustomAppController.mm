//
//  AppDelegate.m
//  IOSDemo
//
//  Created by 雪鲤鱼 on 15/6/29.
//  Copyright (c) 2015年 yijie. All rights reserved.
//

#import "CustomAppController.h"
#import <OnlineAHelper/YiJieOnlineHelper.h>


@interface CustomAppController ()

@end

IMPL_APP_CONTROLLER_SUBCLASS (CustomAppController)

@implementation CustomAppController

- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation NS_AVAILABLE_IOS(4_2){
    BOOL yjResult = [[YJAppDelegae Instance] application:application openURL:url sourceApplication:sourceApplication annotation:annotation];
    return yjResult;
}
- (void)applicationDidEnterBackground:(UIApplication *)application {
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
    NSLog(@"demo application applicationDidEnterBackground！");
    [super applicationDidEnterBackground : application];
    [[YJAppDelegae Instance] applicationDidEnterBackground:application];
}

- (void)applicationWillEnterForeground:(UIApplication *)application {
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
    NSLog(@"demo application applicationWillEnterForeground！");
   [super applicationWillEnterForeground : application];
   [[YJAppDelegae Instance] applicationWillEnterForeground:application];
    
}
- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    // Override point for customization after application launch.
    [super application: application didFinishLaunchingWithOptions : launchOptions];

    return [[YJAppDelegae Instance] application:application didFinishLaunchingWithOptions:launchOptions];

}
- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken {
    
    [[YJAppDelegae Instance] application:application didRegisterForRemoteNotificationsWithDeviceToken:deviceToken];

}
- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo {
    
    [[YJAppDelegae Instance] application:application didReceiveRemoteNotification:userInfo];

    
}
- (NSUInteger)application:(UIApplication *)application supportedInterfaceOrientationsForWindow:(UIWindow *)window {
    
    NSLog(@"demo application supportedInterfaceOrientationsForWindow！");
    return [[YJAppDelegae Instance] application:application supportedInterfaceOrientationsForWindow:window];
   
    
}
- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url{
    // Will be deprecated at some point, please replace with application:openURL:sourceApplication:annotation:
    return  [[YJAppDelegae Instance] application:application handleOpenURL:url];

}

@end
