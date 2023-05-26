using System;

namespace CodeBase.Data.Progress
{   
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string InitialLevel)
        {
            WorldData = new WorldData(InitialLevel);
        }
    }
}