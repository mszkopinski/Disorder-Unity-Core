namespace UnityCore
{
    public class GameEvent
    {
        public readonly string Name;
        public readonly GameEventTypes Type;
        public double Probability { get; private set; }

        public GameEvent(string name, GameEventTypes type = GameEventTypes.Multiple, double probability = 100.0)
        {
            Name = name;
            Type = type;
            Probability = probability;
        }

        public void SetEventProbability(double probability)
        {
            if (probability < 0.0 || probability > 100.0)
                return;

            Probability = probability;
        }
    }
}
