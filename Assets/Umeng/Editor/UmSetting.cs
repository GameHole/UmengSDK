using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using MiniGameSDK;

namespace Umeng
{
	public class UmSetting:IParamSettng
	{
        //[MenuItem("友盟/设置参数")]
        static void Set()
        {
            UmParameterEditor.SetDebugXml(AssetHelper.CreateOrGetAsset<UmParameter>());
            var dc = XmlHelper.GetAndroidManifest();
            dc.SetPermission("android.permission.ACCESS_NETWORK_STATE");
            dc.SetPermission("android.permission.INTERNET");
            dc.SetPermission("android.permission.READ_PHONE_STATE");
            dc.SetPermission("android.permission.ACCESS_WIFI_STATE");
            dc.Save();
            GradleHelper.Open().Save();
            GooglePlayServices.PlayServicesResolver.Resolve(null, true);
            Google.IOSResolver.AutoInstallCocoapods();
        }
        [MenuItem("Assets/转换友盟时间key表")]
        static void Read()
        {
            var asset = Selection.activeObject as TextAsset;
            StringBuilder builder = new StringBuilder();
            builder.Append("public static class EveKeys\n{");
            foreach (var item in asset.text.Split('\n'))
            {
                string key = item.Trim();
                builder.Append($"public static readonly string {key} = \"{key}\";\n");
            }
            builder.Append("}");
            string dir = "Assets/Scripts/UM_GenratedScripts";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText($"{dir}/EveKeys.cs", builder.ToString());
            AssetDatabase.Refresh();
        }

        public void SetParam()
        {
            Set();
        }
    }
}
