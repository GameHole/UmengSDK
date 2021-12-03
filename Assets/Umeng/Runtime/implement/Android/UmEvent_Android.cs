using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace Umeng
{
#if UNITY_ANDROID
    public class UmEvent_Android : IAnalyzeEvent,IInitializable
    {
        AndroidJavaClass umCfg;
        AndroidJavaClass agent;
        public void Initialize()
        {
            //var set = AScriptableObject.Get<UmParameter>();
            //Debug.Log($"umeng start init appid = {set.android.appid}");
            //umCfg = new AndroidJavaClass("com.umeng.commonsdk.UMConfigure");
            agent = new AndroidJavaClass("com.umeng.analytics.MobclickAgent");
            //AndroidJavaClass mode = new AndroidJavaClass("com.umeng.analytics.MobclickAgent$PageMode");
            //umCfg.CallStatic("setLogEnabled", set.android.debug);
            //umCfg.CallStatic("init",ActivityGeter.GetApplication(), set.android.appid, set.android.channal, umCfg.GetStatic<int>("DEVICE_TYPE_PHONE"), null);
            //umCfg.CallStatic("setProcessEvent", true);
            //agent.CallStatic("setPageCollectionMode", mode.GetStatic<AndroidJavaObject>("AUTO"));
            //mode.Dispose();
        }

        public void SetEvent(string key)
        {
            //Debug.Log("set");
            agent.CallStatic("onEvent", ActivityGeter.GetApplication(), key);
        }

        public void SetEvent(string key, Dictionary<string, string> value)
        {
            var act = ActivityGeter.GetApplication();
            if (act != null)
            {
                using (var map = ToJavaHashMap(value))
                {
                    agent.CallStatic("onEvent", act, key, map);
                }
            }
        }
        AndroidJavaObject ToJavaHashMap(Dictionary<string, string> dic)
        {
            var hashMap = new AndroidJavaObject("java.util.HashMap");
            foreach (var entry in dic)
            {
                hashMap.Call<AndroidJavaObject>("put", entry.Key, entry.Value);
            }
            return hashMap;
        }
    }
#endif
}
