using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Disorder.Unity.Core.Extensions.Await
{
    public static class UnityIEnumeratorAwaitExtensions
    {
        public static UnityCoroutineAwaiter<AssetBundle> GetAwaiter(this AssetBundleCreateRequest request)
        {
            var awaiter = new UnityCoroutineAwaiter<AssetBundle>();
            UnityThreadingUtility.RunOnUnityScheduler(() => UnityAsyncCoroutineRunner.Instance.StartCoroutine(AssetBundleCreateRequest(awaiter, request)));
            return awaiter;
        }

        static IEnumerator AssetBundleCreateRequest(UnityCoroutineAwaiter<AssetBundle> awaiter, AssetBundleCreateRequest request)
        {
            yield return request;
            awaiter.Complete(request.assetBundle, null);
        }

        public static UnityCoroutineAwaiter<Object> GetAwaiter(this AssetBundleRequest request)
        {
            var awaiter = new UnityCoroutineAwaiter<Object>();
            UnityThreadingUtility.RunOnUnityScheduler(() => UnityAsyncCoroutineRunner.Instance.StartCoroutine(AssetBundleRequest(awaiter, request)));
            return awaiter;
        }

        static IEnumerator AssetBundleRequest(UnityCoroutineAwaiter<Object> awaiter, AssetBundleRequest request)
        {
            yield return request;
            awaiter.Complete(request.asset, null);
        }

        public static UnityCoroutineAwaiter<Object> GetAwaiter(this ResourceRequest request)
        {
            var awaiter = new UnityCoroutineAwaiter<Object>();
            UnityThreadingUtility.RunOnUnityScheduler(() => UnityAsyncCoroutineRunner.Instance.StartCoroutine(ResourceRequest(awaiter, request)));
            return awaiter;
        }

        static IEnumerator ResourceRequest(UnityCoroutineAwaiter<Object> awaiter, ResourceRequest request)
        {
            yield return request;
            awaiter.Complete(request.asset, null);
        }

        public static UnityCoroutineAwaiter GetAwaiter(this WaitForSeconds instruction)
        {
            return GetVoidAwaiter(instruction);
        }

        public static UnityCoroutineAwaiter GetAwaiter(this WaitForEndOfFrame instruction)
        {
            return GetVoidAwaiter(instruction);
        }

        public static UnityCoroutineAwaiter GetAwaiter(this WaitForFixedUpdate instruction)
        {
            return GetVoidAwaiter(instruction);
        }

        static UnityCoroutineAwaiter GetVoidAwaiter(object instruction)
        {
            var awaiter = new UnityCoroutineAwaiter();
            UnityThreadingUtility.RunOnUnityScheduler(() => UnityAsyncCoroutineRunner.Instance.StartCoroutine(ReturnVoid(awaiter, instruction)));
            return awaiter;
        }

        static IEnumerator ReturnVoid(UnityCoroutineAwaiter awaiter, object instruction)
        {
            yield return instruction;
            awaiter.Complete(null);
        }
    }
}