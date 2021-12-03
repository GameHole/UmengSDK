namespace Umeng
{
    [System.Serializable]
    public class UmParameterBase
    {
        public string appid;
        public string channal;
        public bool debug;
        public bool useRemoteCtrl;
    }
    public class UmParameter : AScriptableObject
    {
        public override string filePath => "友盟参数";
        public UmParameterBase android;
        public UmParameterBase ios;
    }
}
