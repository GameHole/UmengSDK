#if UNITY_ANDROID
using System.Collections.Generic;
using UnityEngine;
namespace Umeng
{
    public class UmRemoteCtrl_Android : IRemoteCtrl,IInitializable
    {
        AndroidJavaObject cfgInst;
        public string GetConfig(string key)
        {
            if (cfgInst == null)
                return null;
            return cfgInst.Call<string>("getConfigValue", key);
        }

        public void Initialize()
        {
            using(AndroidJavaClass cfg = new AndroidJavaClass("com.umeng.cconfig.UMRemoteConfig"))
            {
                cfgInst = cfg.CallStatic<AndroidJavaObject>("getInstance");
            }
        }
    }
}
#endif
