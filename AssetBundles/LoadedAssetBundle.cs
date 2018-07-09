using System;
using UnityEngine;

namespace UnityCore.AssetBundles
{
    public class LoadedAssetBundle
    {
        public readonly AssetBundle Bundle;
        public readonly int ReferenceCount;

        public LoadedAssetBundle(AssetBundle assetBundle)
        {
            Bundle = assetBundle;
            ReferenceCount = 1;
        }

        internal event Action BundleUnloaded;

        internal void OnBundleUnloaded()
        {
            Bundle.Unload(false);
            BundleUnloaded?.Invoke();
        }
    }
}