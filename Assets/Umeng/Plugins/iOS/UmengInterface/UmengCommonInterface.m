#import<UMCommon/MobClick.h>
#import <Foundation/Foundation.h>
#import <UMCommonLog/UMCommonLogManager.h>
#import <UMCommon/UMCommon.h>
void _Init(const char* appid,const char * channal,bool isDebug){
    NSString *appstr = [NSString stringWithCString:appid encoding:NSUTF8StringEncoding];
    NSString *cnstr = [NSString stringWithCString:channal encoding:NSUTF8StringEncoding];
    if(isDebug)
        [UMCommonLogManager setUpUMCommonLogManager];
    [UMConfigure setLogEnabled:isDebug];
    [UMConfigure initWithAppkey:appstr channel:cnstr];
}
void _Event(const char* key)
{
  NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
  [MobClick event:keyStr];
}
void _EventWithValues(const char* key,const char* valueStr)
{
  NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
  NSString *vStr = [NSString stringWithCString:valueStr encoding:NSUTF8StringEncoding];
  NSArray *arr = [vStr componentsSeparatedByString:@";"];
    
  NSMutableDictionary *dict1 = [NSMutableDictionary dictionary];
  for(int i=0; i<arr.count ; i++)
  {
      
    NSArray *kvs = [arr[i] componentsSeparatedByString:@","];
    if(kvs.count>1)
          [dict1 setValue:kvs[1] forKey:kvs[0]];
  }
  [MobClick event:keyStr attributes:dict1];
}