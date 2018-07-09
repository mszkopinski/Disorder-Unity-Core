using UnityEngine;

namespace UnityCore
{
    public class PersistentMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;

                    if (_instance == null)
                    {
                        Debug.LogError("instance of " + typeof(T) + " is needed in the scene.");
                    }
                }

                return _instance;
            }
        }

        protected static bool _isCreated;
        protected static T _instance;

        protected virtual void Awake()
        {
            if (_isCreated)
                return;

            DontDestroyOnLoad(gameObject);
            _isCreated = true;
        }
    }
}
