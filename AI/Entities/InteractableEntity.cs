using System.Collections.Generic;
using UnityEngine;

namespace UnityCore
{
    public abstract class InteractableEntity : MonoBehaviour, IMouseListener
    {
        protected List<IAInteligenceEntity> ActionPerformers = new List<IAInteligenceEntity>();

        [SerializeField]
        public float InteractionRange = 0.5f;

        public abstract void Interact(IAInteligenceEntity stateMachineHolder);

        public virtual void OnTriggerEnter(Collider col)
        {
            var performerComponent = col.GetComponent(typeof(IAInteligenceEntity));

            if (performerComponent == null)
                return;

            var performer = ((IAInteligenceEntity) performerComponent);
            ActionPerformers.Add(performer);

            Interact(performer);
        }

        public virtual void OnTriggerExit(Collider col)
        {
            var performerComponent = col.GetComponent(typeof(IAInteligenceEntity));

            if (performerComponent == null)
                return;

            var actionPerformer = (IAInteligenceEntity) performerComponent;

            if (ActionPerformers.Contains(actionPerformer))
                ActionPerformers.Remove(actionPerformer);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, InteractionRange);
        }

        public virtual void OnMouseEnter()
        {
        }

        public virtual void OnMouseExit()
        {
        }

        public virtual void OnMouseOver()
        {
        }
    }
}