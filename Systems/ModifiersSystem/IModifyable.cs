using System.Collections.Generic;

namespace Disorder.Unity.Core
{
    public interface IModifyable
    {
        List<Modifier> Modifiers { get; set; }

        void AddModifier(Modifier mod);
        void RemoveModifier(Modifier mod);
        void ApplyModifiers();
    }
}