using UnityEngine;

namespace UnityCore
{
    [RequireComponent(typeof(Camera))]
    public abstract class BaseCameraController : MonoSingleton<BaseCameraController>
    {
        protected Camera Cam;
        protected Transform TargetTransform;

        void Awake()
        {
            Cam = GetComponent<Camera>() ?? Camera.main;
        }

        protected virtual void LateUpdate()
        {
            if (TargetTransform != null)
            {
                //var targetPos = TargetTransform.position + positionOffset;
                //Cam.transform.position = Vector3.SmoothDamp(Cam.transform.position, targetPos, ref velocity, isRotating ? rotationSmoothSpeed : smoothSpeed);

                //if (lookAtTarget)
                //    Cam.transform.LookAt(TargetTransform);
            }
        }
    }
}