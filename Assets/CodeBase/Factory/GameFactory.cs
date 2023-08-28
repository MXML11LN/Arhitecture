using System;
using System.Collections.Generic;
using CodeBase.AssetManagement;
using CodeBase.GamePlay.Enemy;
using CodeBase.GamePlay.Hero;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        private GameObject Hero { get; set;}

        [Inject]
        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assets = assetProvider;
            _staticData = staticData;
        }

        public GameObject CreateHero(GameObject at)
        {
            Hero = InstantiateRegistered(AssetPath.HeroPrefabPath, at.transform.position);
            return Hero;
        }

        public GameObject CreateHUD()
        {
            GameObject hudGameObject = InstantiateRegistered(AssetPath.HUDPrefabPath);
            hudGameObject.GetComponent<ActorUI>().Construct(Hero.GetComponent<IHealth>());
            return hudGameObject;
        }

        public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeId);
            GameObject monster = Object.Instantiate(monsterData.MonsterPrefab,parent.transform.position,Quaternion.identity,parent);
            IHealth health = monster.GetComponent<IHealth>();
            health.CurrentHp = monsterData.Hp;
            health.MaxHp = monsterData.Hp;
            
            monster.GetComponent<EnemyUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(Hero.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.moveSpeed;
            
            monster.GetComponentInChildren<LootSpawner>().Construct(this);
            
            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(Hero.transform);
            attack.Clevage = monsterData.Clevage;
            attack.AttackCoolDown = monsterData.AttackCoolDown;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            return monster;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreateLoot() => 
            InstantiateRegistered(AssetPath.Loot);

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter) 
                ProgressWriters.Add(progressWriter);
            ProgressReaders.Add(progressReader);
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
    }
}