using System;
using CodeBase.Data.Progress;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Logic.Spawn
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        private string _id;
        private bool _slain;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
            {
                throw new NotImplementedException();
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
            {
                progress.KillData.ClearedSpawners.Add(_id);
            }
        }
    }
}