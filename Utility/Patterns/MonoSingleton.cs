using UnityEngine;

namespace UnityCore
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;

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
    }
}