using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCore
{
    public abstract class Panel : MonoSingleton<Panel>
    {
        [Header("General")]
        public bool IsDisabledByDefault = false;
        public bool CleanListenersOnDisable = false;

        protected virtual void OnEnable()
        {
            if (IsDisabledByDefault)
                StartCoroutine(DisablePanelInNextFrame());
        }

        IEnumerator DisablePanelInNextFrame()
        {
            yield return new WaitForEndOfFrame();
            ClosePanel();
        }

        public virtual void ShowPanel()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        public virtual void ClosePanel()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Remove all listeners from children buttons. You must remove other listeners manually.
        /// </summary>
        protected virtual void OnDisable()
        {
            // need to check whether object is active in hierarchy because some panels get disabled by default at the start
            if (gameObject.activeInHierarchy && CleanListenersOnDisable)
            {
                foreach (var btn in gameObject.GetComponentsInChildren<Button>())
                {
                    btn.onClick.RemoveAllListeners();
                }
            }
        }
    }
}
