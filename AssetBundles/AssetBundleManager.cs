using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityCore.AssetBundles
{
    public class AssetBundleManager : MonoSingleton<AssetBundleManager>
    {
        readonly Dictionary<string, LoadedAssetBundle> loadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();

        // Check for dependencies later
        public AssetBundle GetLoadedAssetBundle(string bundleName)
        {
            loadedAssetBundles.TryGetValue(bundleName, out var bundle);

            return bundle?.Bundle;
        }

        public void LoadAsssetBundleFromFile(string bundleName)
        {
            var path = UnityFsHelper.GetBundlePath(bundleName);

            if (!File.Exists(path))
            {
                Debug.LogWarning($"Bundle {bundleName} doesn't exist in streaming assets.");
                return;
            }

            LoadAssetBundleAsync(path).RunTaskAsync();
        }

        async Task LoadAssetBundleAsync(string bundlePath)
        {
            var assetBundle = await AssetBundle.LoadFromFileAsync(bundlePath);

            if (assetBundle == null)
                return;

            loadedAssetBundles.Add(assetBundle.name, new LoadedAssetBundle(assetBundle));
        }
    }
}