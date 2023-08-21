using System;
using CodeBase.Data.Progress;
using CodeBase.Factory;
using CodeBase.GamePlay.Enemy;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Spawn
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId monsterTypeId;
        private string _id;
        private bool _slain;
        private IGameFactory _factory;
        public bool Slain => _slain;
        private EnemyDeath _death;

        [Inject]
        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            GameObject monster = _factory.CreateMonster(monsterTypeId, transform);
            _death = monster.GetComponent<EnemyDeath>();
            _death.Happened += Slay;
        }

        private void Slay()
        {
            _slain = true;
            if (_death != null)
                _death.Happened -= Slay;
        }


        public void UpdateProgress(PlayerProgress progress)
        {
            if (Slain)
            {
                progress.KillData.ClearedSpawners.Add(_id);
            }
        }
    }
}