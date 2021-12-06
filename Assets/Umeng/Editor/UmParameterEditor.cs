using MiniGameSDK;
using System.Collections.Generic;
using UnityEngine;
namespace Umeng
{
    [UnityEditor.CustomEditor(typeof(UmParameter))]
    class UmParameterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SetDebugXml(target as UmParameter);
            UMInitSetter.Set();
            base.OnInspectorGUI();
        }
        static readonly string umMark = "umeng debug";
        public static void SetDebugXml(UmParameter o)
        {
            if (o.android.debug)
            {
                SetXml(o);
            }
            else
            {
                RemoveXml();
            }
        }
        static void SetXml(UmParameter um)
        {
            var doc = XmlHelper.GetAndroidManifest();
            var intnt = doc.FindNode("/manifest/application/activity/intent-filter", "android:label", umMark);
            if (intnt == null)
            {
                var act = doc.SelectSingleNode("/manifest/application/activity");
                intnt = doc.CreateElement("intent-filter");
                intnt.CreateAttribute("label", "umeng debug");
                var action = doc.CreateElement("action");
                action.CreateAttribute("name", "android.intent.action.VIEW");
                intnt.AppendChild(action);
                var category = doc.CreateElement("category");
                category.CreateAttribute("name", "android.intent.category.DEFAULT");
                intnt.AppendChild(category);
                category = doc.CreateElement("category");
                category.CreateAttribute("name", "android.intent.category.BROWSABLE");
                intnt.AppendChild(category);
                var data = doc.CreateElement("data");
                data.CreateAttribute("scheme", $"um.{um.android.appid}");
                intnt.AppendChild(data);
                act.AppendChild(intnt);
            }
            else
            {
                var data = doc.SelectSingleNode("/manifest/application/activity/intent-filter/data");
                data.Attributes["android:scheme"].Value = $"um.{um.android.appid}";
            }
            doc.Save();
        }
        static void RemoveXml()
        {
            var doc = XmlHelper.GetAndroidManifest();
            var intnt = doc.FindNode("/manifest/application/activity/intent-filter", "android:label", umMark);
            if (intnt != null)
            {
                var act = doc.SelectSingleNode("/manifest/application/activity");
                act.RemoveChild(intnt);
                doc.Save();
            }
        }
    }
}
