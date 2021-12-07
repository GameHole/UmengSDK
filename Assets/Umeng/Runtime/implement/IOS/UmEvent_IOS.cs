#if UNITY_IOS
using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System.Runtime.InteropServices;
using System.Text;

namespace Umeng
{
    public class UmEvent_IOS : IAnalyzeEvent,IMenualInitor
    {
        [DllImport("__Internal")]
        static extern void _Event(string key);
        [DllImport("__Internal")]
        static extern void _EventWithValues(string key,string mapKeyStr);
        [DllImport("__Internal")]
        static extern void _Init(string appid, string channal, bool isDebug);
        
        StringBuilder builder = new StringBuilder();
        public void SetEvent(string key)
        {
            if (!PlatfotmHelper.isEditor())
                _Event(key);
        }

        public void SetEvent(string key, Dictionary<string, string> value)
        {

            builder.Clear();
            foreach (var item in value)
            {
                builder.Append(item.Key);
                builder.Append(',');
                builder.Append(item.Value);
                builder.Append(';');
            }
            if (!PlatfotmHelper.isEditor())
                _EventWithValues(key, builder.ToString());
        }

        public void Init()
        {
            var set = AScriptableObject.Get<UmParameter>();
            if (set.ios.isLateInit && !PlatfotmHelper.isEditor())
                _Init(set.ios.appid, set.ios.channal, set.ios.debug);
        }
    }
}
#endif
