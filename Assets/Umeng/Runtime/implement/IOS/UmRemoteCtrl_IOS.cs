#if UNITY_IOS
using System.Runtime.InteropServices;
using UnityEngine;
using MiniGameSDK;
namespace Umeng
{
    public class UmRemoteCtrl_IOS : IRemoteCtrl, IInitializable
    {
        [DllImport("__Internal")]
        static extern string _GetConfig(string key);
        [DllImport("__Internal")]
        static extern void _InitRemote();
        UmParameter um;
        public string GetConfig(string key)
        {
            if (um.ios.useRemoteCtrl && !PlatfotmHelper.isEditor())
                return _GetConfig(key);
            return null;
        }

        public void Initialize()
        {
            um = AScriptableObject.Get<UmParameter>();
            if (!PlatfotmHelper.isEditor()&& um.ios.useRemoteCtrl)
            {
                _InitRemote();
            }
        }
       
    }
}
#endif
