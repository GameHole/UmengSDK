using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO.Compression;
using Microsoft.Win32;
using System.Diagnostics;

namespace Umeng
{
    public class TestBuildPostProcessor
    {
        [MenuItem("test/test")]
        static void read()
        {
            string d = "Assets/zTest/test.txt";
            if (File.Exists(d))
                File.Delete(d);
            File.Copy("Assets/test.txt", d);
            var g = GradleHelper.Open("Assets/zTest/test.txt");
            g.Root.containNewlineCharactersBeforeBeginBrace = true;
            g.Save();
            AssetDatabase.Refresh();
        }
        //[MenuItem("test/test")]
        //static void test()
        //{
        //    ComprassDirToRar(@"E:\WorkSpace\unityProjects\Gits\UmengSDK\Assets\Plugins", @"E:\WorkSpace\Webs\APK\1.rar");
        //}
        [PostProcessBuildAttribute(99)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target == BuildTarget.iOS)
            {
                ComprassDirToRar(path, @"E:\WorkSpace\Webs\APK\1.rar");
            }
        }
        public static string GetRarExePath()
        {
            using (var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe"))
            {
                if (reg == null)
                    return null;
                return reg.GetValue("").ToString();
            }
        }
        public static void ComprassDirToRar(string srcDir,string destRarFile)
        {
            var rarExe = GetRarExePath();
            if (!string.IsNullOrEmpty(rarExe))
            {
                var dir = Path.GetDirectoryName(destRarFile);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                using (var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = rarExe,
                        Arguments = $"a -afzip -m0 -ep1 -r \"{destRarFile}\" \"{srcDir}\"",
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = dir,
                        CreateNoWindow = false,
                    }
                })
                {
                    process.Start();
                    process.WaitForExit();
                    process.Close();
                }
            }
        }
    }
}
