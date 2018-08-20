using UnityEngine;

namespace UnityCore
{
    /// <summary>
    /// Class created for debugging Unity built-in methods like Start, Awake, Update etc.
    /// Unity methods called in MonoBehaviour do not work properly with debuggers
    /// </summary>
    public abstract class MonoBehaviourDebug : MonoBehaviour
    {
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void Update() { }
        protected virtual void LateUpdate() { }
        protected virtual void FixedUpdate() { }
    }
}