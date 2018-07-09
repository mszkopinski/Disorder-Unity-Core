using UnityEngine;

namespace UnityCore
{
    public class CoroutinePerformer : MonoBehaviour
    {
        static CoroutinePerformer _instance;

        public static CoroutinePerformer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("UnityAsyncCoroutineRunner").AddComponent<CoroutinePerformer>();
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