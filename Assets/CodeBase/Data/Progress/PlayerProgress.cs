using System;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public HeroState HeroState;

        public PlayerProgress(string InitialLevel)
        {
            WorldData = new WorldData(InitialLevel);
            HeroState = new HeroState();
        }
    }
}