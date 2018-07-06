namespace Disorder.Unity.Core
{
    public class Modifier
    {
        public readonly IModifyable TargetStat;
        public readonly float ModifierValue;
        public readonly ModifierType ModifierType;

        public Modifier(IModifyable targetStat,  ModifierType modifierType, float modifierValue)
        {
            TargetStat = targetStat;
            ModifierType = modifierType;
            ModifierValue = modifierValue;
        }
    }
}