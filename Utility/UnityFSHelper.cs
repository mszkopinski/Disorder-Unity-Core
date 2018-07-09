using System.IO;
using UnityEngine;

namespace UnityCore
{
    public static class UnityFsHelper
    {
        public static string GetBundlePath(string bundleName) => Path.Combine(Application.streamingAssetsPath, bundleName);
        public static string GetPersistenDataPath(string fileName) => Path.Combine(Application.persistentDataPath, fileName);
        public static string GetStreamingAssetPath(string assetName) => Path.Combine(Application.streamingAssetsPath, assetName);
    }
}