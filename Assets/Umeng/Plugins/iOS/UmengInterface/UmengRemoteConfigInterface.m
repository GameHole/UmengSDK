#import <UMRemoteConfig/UMRemoteConfig.h>
#import <Foundation/Foundation.h>
#import <UMRemoteConfig/UMRemoteConfigSettings.h>
 char* _GetConfig(const char* key){
    NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
    //NSLog(@"%@",keyStr);
    NSString *valueStr= (NSString *)[UMRemoteConfig configValueForKey:keyStr];
    if(valueStr==nil)
        return nil;
    //NSLog(@"str %@",valueStr);
    const char* navStr= [valueStr UTF8String];
    char* res=(char*)malloc(strlen(navStr)+1);
    strcpy(res, navStr);
    return  res;
 }
@interface RemoteCallBack : NSObject<UMRemoteConfigDelegate>
@end
@implementation RemoteCallBack
-(void)remoteConfigFetched:(UMRemoteConfigFetchStatus)status error:(nullable NSError*)error userInfo:(nullable id)userInfo
{
    NSLog(@"remoteConfigFetched%@",error);
}
-(void)remoteConfigActivated:(UMRemoteConfigActiveStatus)status error:(nullable NSError*)error userInfo:(nullable id)userInfo
{
    NSLog(@"remoteConfigActivated%@",error);
}
-(void)remoteConfigReady:(UMRemoteConfigActiveStatus)status error:(nullable NSError*)error userInfo:(nullable id)userInfo
{
    NSLog(@"remoteConfigReady %@",error );
}
@end
RemoteCallBack* inst;
void _InitRemote()
{
    if(inst==nil)
    {
        inst=[[RemoteCallBack alloc]init];
        UMRemoteConfigSettings* set=[UMRemoteConfigSettings new];
        set.activateAfterFetch=true;
        [UMRemoteConfig remoteConfigWithDelegate:inst withConfigSettings:set];
        //[UMRemoteConfig activateWithCompletionHandler:nil];
    }
}
