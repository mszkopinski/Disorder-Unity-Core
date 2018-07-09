using UnityEngine;

namespace UnityCore
{
    public static class TransformExtensions
    {
        public static Transform RemoveAllChildren(this Transform transform)
        {
            foreach (Transform children in transform)
            {
                Object.Destroy(children.gameObject);
            }

            return transform;
        }
    }
}