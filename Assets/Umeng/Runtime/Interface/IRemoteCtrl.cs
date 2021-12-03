using System.Collections.Generic;
using UnityEngine;
namespace Umeng
{
	public interface IRemoteCtrl:IInterface
	{
        string GetConfig(string key);
	}
}
