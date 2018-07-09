using System.Collections.Generic;

namespace UnityCore
{
    public interface IModifyable
    {
        List<Modifier> Modifiers { get; set; }

        void AddModifier(Modifier mod);
        void RemoveModifier(Modifier mod);
        void ApplyModifiers();
    }
}