using System.Collections.Generic;

namespace UnityCore
{
    public interface IModifiersRepository
    {
        List<Modifier> Modifiers { get; set; }

        void ApplyModifiersToActivity(Activity activity);
        void LoadModifiers(ISerializable data);
        void SaveModifiers();
    }
}