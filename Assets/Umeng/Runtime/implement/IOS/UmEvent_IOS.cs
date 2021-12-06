#if UNITY_IOS
using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System.Runtime.InteropServices;
using System.Text;

namespace Umeng
{
    public class UmEvent_IOS : IAnalyzeEvent
    {
        [DllImport("__Internal")]
        static extern void _Event(string key);
        [DllImport("__Internal")]
        static extern void _EventWithValues(string key,string mapKeyStr);
        StringBuilder builder = new StringBuilder();
        public void SetEvent(string key)
        {
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
            _EventWithValues(key, builder.ToString());
        }
    }
}
#endif
