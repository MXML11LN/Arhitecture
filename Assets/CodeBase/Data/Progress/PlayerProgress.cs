﻿using System;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public KillData KillData;

        public HeroState HeroState;
        public HeroStats HeroStats;


        public PlayerProgress(string InitialLevel)
        {
            WorldData = new WorldData(InitialLevel);
            HeroState = new HeroState();
            HeroStats = new HeroStats();
            KillData = new KillData();
        }
    }
}