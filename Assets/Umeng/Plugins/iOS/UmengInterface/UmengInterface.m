#import<UMCommon/MobClick.h>
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
    [dict1 setValue:kvs[1] forKey:kvs[0]];
  }
  [MobClick event:keyStr attributes:dict1];
}