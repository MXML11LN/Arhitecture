using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId,MonsterStaticData> _monsters = new Dictionary<MonsterTypeId, MonsterStaticData>();
        private const string StaticdataMonstersPath = "StaticData/Monsters/";

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(StaticdataMonstersPath)
                .ToDictionary(x=>x.TypeId,x=>x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId monsterType) => 
            _monsters.TryGetValue(monsterType, out MonsterStaticData staticData)
                ? staticData
                : null;
    }
}