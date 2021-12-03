using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
public class Log : MonoBehaviour
{
    IAnalyzeEvent analyze;
    Umeng.IRemoteCtrl remote;
    // Start is called before the first frame update
    void Start()
    {
        analyze.SetEvent("test");
        analyze.SetEvent("test1", new Dictionary<string, string> { { "a", "b" } });
        Debug.Log($"remote cfd::{remote.GetConfig("test")}");
    }

}
