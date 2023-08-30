using System;

namespace CodeBase.Data.Progress
{
    [Serializable]
    public class LootData
    {
        public int collected;
        public Action Changed;

        public void Collect(Loot loot)
        {
            collected += loot.value;
            Changed?.Invoke();
        }
    }
}