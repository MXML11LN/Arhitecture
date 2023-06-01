using System;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class HeroState
    {
        public float CurrentHp;
        public float MaxHp;
        public void ResetHP() => CurrentHp = MaxHp;
    }
}