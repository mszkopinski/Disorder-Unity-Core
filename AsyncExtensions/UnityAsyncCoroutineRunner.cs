using UnityEngine;

namespace Disorder.Unity.Core.Extensions.Await
{
    public class UnityAsyncCoroutineRunner : MonoBehaviour
    {
        static UnityAsyncCoroutineRunner _instance;

        public static UnityAsyncCoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("UnityAsyncCoroutineRunner").AddComponent<UnityAsyncCoroutineRunner>();
                }

                return _instance;
            }
        }

        void Awake()
        {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(this);
        }
    }
}