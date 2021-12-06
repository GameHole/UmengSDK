using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using UnityEditor;
using System.IO;

namespace Umeng
{
    public class UMInitSetter : IParamSettng
    {
        public void SetParam()
        {
            Set();
        }
        public static void Set()
        {
            var cp = AssetHelper.CreateOrGetAsset<UmParameter>();
            var f = AssetDatabase.GUIDToAssetPath("1dc25faf7e342304884b9bec51342d20");//UmengProInitor.txt
            var dst = $"Assets/Plugins/Android/{Path.GetFileNameWithoutExtension(f)}.java";
            IOHelper.CopyFileWithReplease(f, dst, new KeyValuePair<string, string>("##DEBUG##", cp.android.debug.ToString().ToLower()),
                new KeyValuePair<string, string>("##APPID##", cp.android.appid),
                new KeyValuePair<string, string>("##APPCHANNAL##", cp.android.channal),
                new KeyValuePair<string, string>("##USE_REMOTE_CTRL##", cp.android.useRemoteCtrl.ToString().ToLower()),
                new KeyValuePair<string, string>("##LATE_INIT##", cp.android.isLateInit.ToString().ToLower()));
            JavaHelper.RegistJavaInterface(dst);
        }
    }
}
