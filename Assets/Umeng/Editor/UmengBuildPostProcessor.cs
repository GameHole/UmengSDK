//
// Copyright (c) 2017 eppz! mobile, Gergely Borbás (SP)
//
// http://www.twitter.com/_eppz
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
// OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
namespace Umeng
{
    public class UmengBuildPostProcessor
    {
        [PostProcessBuildAttribute(1)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target == BuildTarget.iOS)
            {
                // Read.
                string projectPath = PBXProject.GetPBXProjectPath(path);
                PBXProject project = new PBXProject();
                project.ReadFromString(File.ReadAllText(projectPath));
#if UNITY_2019_3_OR_NEWER
            string targetName = project.GetUnityMainTargetGuid();
#else
                string targetName = PBXProject.GetUnityTargetName();
#endif
                string targetGUID = project.TargetGuidByName(targetName);

                AddFrameworks(project, targetGUID);

                // Write.
                File.WriteAllText(projectPath, project.WriteToString());

                InjectInitCode(path);

            }
        }

        static void AddFrameworks(PBXProject project, string targetGUID)
        {
            // Frameworks 

            project.AddFrameworkToProject(targetGUID, "libz.dylib", false);
            project.AddFrameworkToProject(targetGUID, "libsqlite3.tbd", false);
            project.AddFrameworkToProject(targetGUID, "CoreTelephony.framework", false);
            project.AddFrameworkToProject(targetGUID, "SystemConfiguration.framework", false);
            project.AddFrameworkToProject(targetGUID, "libc++.tbd", false);
            project.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");
        }
        static void InjectInitCode(string path)
        {
            var set = AssetHelper.CreateOrGetAsset<UmParameter>();
            var appPath = Path.Combine(path, "Classes/UnityAppController.mm");
            var mmfile = GradleHelper.Open(appPath);
            mmfile.Root.containNewlineCharactersBeforeBeginBrace = true;
            mmfile.Root.InsertValue(0, "#import <UMCommon/UMCommon.h>");
            var fish = mmfile.Root.FindNode("- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions");
            fish.InsertValue(1, $"[UMConfigure initWithAppkey:@\"{set.ios.appid}\" channel:@\"{set.ios.channal}\"];");
            if (set.ios.debug)
            {
                mmfile.Root.InsertValue(0, "#import <UMCommonLog/UMCommonLogManager.h>");
                fish.InsertValue(1, "[UMConfigure setLogEnabled:YES];");
                fish.InsertValue(1, "[UMCommonLogManager setUpUMCommonLogManager];");
            }
            mmfile.Save();
        }
    }
}
