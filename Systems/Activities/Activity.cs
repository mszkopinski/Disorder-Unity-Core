using System.Collections;

namespace UnityCore
{
    /// <summary>
    /// It's basically like state but with information who is performing an activity
    /// </summary>
    public abstract class Activity : IState, IEnumerable
    {
        public readonly IAInteligenceEntity ActionPerfomer;

        protected Activity(IAInteligenceEntity actionPerfomer)
        {
            ActionPerfomer = actionPerfomer;
        }

        public IAInteligenceEntity ActionPerformer { get; private set; }

        public virtual void Initialize(IAInteligenceEntity actionPerformer)
        {
            ActionPerformer = actionPerformer;
        }

        public abstract void Enter();

        public abstract void Execute();

        public abstract void Exit();


        /// <summary>
        /// Wanted to implement activities as IEnumerables 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return null;
        }
    }
}