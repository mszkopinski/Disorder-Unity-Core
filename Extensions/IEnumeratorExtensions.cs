using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityCore
{
    public static class EnumeratorExtensions
    {
        public static UnityCoroutineScheduler<AssetBundle> GetAwaiter(this AssetBundleCreateRequest request)
        {
            var awaiter = new UnityCoroutineScheduler<AssetBundle>();
            UnitySchedulerProvider.RunOnUnityScheduler(() => CoroutinePerformer.Instance.StartCoroutine(AssetBundleCreateRequest(awaiter, request)));
            return awaiter;
        }

        static IEnumerator AssetBundleCreateRequest(UnityCoroutineScheduler<AssetBundle> scheduler, AssetBundleCreateRequest request)
        {
            yield return request;
            scheduler.Complete(request.assetBundle, null);
        }

        public static UnityCoroutineScheduler<Object> GetAwaiter(this AssetBundleRequest request)
        {
            var awaiter = new UnityCoroutineScheduler<Object>();
            UnitySchedulerProvider.RunOnUnityScheduler(() => CoroutinePerformer.Instance.StartCoroutine(AssetBundleRequest(awaiter, request)));
            return awaiter;
        }

        static IEnumerator AssetBundleRequest(UnityCoroutineScheduler<Object> scheduler, AssetBundleRequest request)
        {
            yield return request;
            scheduler.Complete(request.asset, null);
        }

        public static UnityCoroutineScheduler<Object> GetAwaiter(this ResourceRequest request)
        {
            var awaiter = new UnityCoroutineScheduler<Object>();
            UnitySchedulerProvider.RunOnUnityScheduler(() => CoroutinePerformer.Instance.StartCoroutine(ResourceRequest(awaiter, request)));
            return awaiter;
        }

        static IEnumerator ResourceRequest(UnityCoroutineScheduler<Object> scheduler, ResourceRequest request)
        {
            yield return request;
            scheduler.Complete(request.asset, null);
        }

        public static UnityCoroutineScheduler GetAwaiter(this WaitForSeconds instruction)
        {
            return GetVoidAwaiter(instruction);
        }

        public static UnityCoroutineScheduler GetAwaiter(this WaitForEndOfFrame instruction)
        {
            return GetVoidAwaiter(instruction);
        }

        public static UnityCoroutineScheduler GetAwaiter(this WaitForFixedUpdate instruction)
        {
            return GetVoidAwaiter(instruction);
        }

        static UnityCoroutineScheduler GetVoidAwaiter(object instruction)
        {
            var awaiter = new UnityCoroutineScheduler();
            UnitySchedulerProvider.RunOnUnityScheduler(() => CoroutinePerformer.Instance.StartCoroutine(ReturnVoid(awaiter, instruction)));
            return awaiter;
        }

        static IEnumerator ReturnVoid(UnityCoroutineScheduler awaiter, object instruction)
        {
            yield return instruction;
            awaiter.Complete(null);
        }
    }
}