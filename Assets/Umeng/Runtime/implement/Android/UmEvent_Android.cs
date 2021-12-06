using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace Umeng
{
#if UNITY_ANDROID
    public class UmEvent_Android : IAnalyzeEvent,IInitializable, IMenualInitor
    {
        AndroidJavaClass agent;
        public void Init()
        {
            var set = AScriptableObject.Get<UmParameter>();
            if (!set.android.isLateInit) return;
            Debug.Log($"umeng init appid = {set.android.appid}");
            AndroidJavaClass umCfg = new AndroidJavaClass("com.umeng.commonsdk.UMConfigure");
            AndroidJavaClass mode = new AndroidJavaClass("com.umeng.analytics.MobclickAgent$PageMode");
            umCfg.CallStatic("init", ActivityGeter.GetApplication(), set.android.appid, set.android.channal, umCfg.GetStatic<int>("DEVICE_TYPE_PHONE"), null);
            umCfg.CallStatic("setProcessEvent", true);
            agent.CallStatic("setPageCollectionMode", mode.GetStatic<AndroidJavaObject>("AUTO"));
            mode.Dispose();
            umCfg.Dispose();
        }
        public void Initialize()
        {
            agent = new AndroidJavaClass("com.umeng.analytics.MobclickAgent");
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
