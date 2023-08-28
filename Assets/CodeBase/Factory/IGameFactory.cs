using System.Collections.Generic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHUD();
        void CleanUp();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Register(ISavedProgressReader progressReader);
        GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        GameObject CreateLoot();
    }
}