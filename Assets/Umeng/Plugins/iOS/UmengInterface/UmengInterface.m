#import<MobClick.h>
void _Event(const char* key)
{
  NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
  [MobClick event:keyStr];
}
void _Event(const char* key,const char*)
{
  NSString *keyStr = [NSString stringWithCString:key encoding:NSUTF8StringEncoding];
  [MobClick event:keyStr];
}