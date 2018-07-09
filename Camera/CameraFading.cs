using UnityEngine;
using Time = UnityEngine.Time;

namespace UnityCore
{
    [RequireComponent(typeof(Camera))]
    public class CameraFading : MonoSingleton<CameraFading>
    {
        public float FadeSpeed = 0.8f;

        [SerializeField] private Texture2D fadeTexture;
        private int drawDepth = -5000;
        private float alpha = 1.0f;
        private FadeDirection fadeDirection = FadeDirection.In;

        void OnGUI()
        {
            alpha += (fadeDirection == FadeDirection.In ? -1 : 1) * FadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }

        public void FadeIn()
        {
            fadeDirection = FadeDirection.In;
        }

        public void FadeOut()
        {
            Debug.Log("I should've faded out.");
            fadeDirection = FadeDirection.Out;
        }
    }

    public enum FadeDirection
    {
        In,
        Out
    }
}