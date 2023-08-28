using System;
using CodeBase.Factory;
using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{ 
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath enemyDeath;
        private IGameFactory _factory;
        
        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }

        private void Start()
        {
            enemyDeath.Happened += SpawnLoot;
        }

        private void SpawnLoot()
        {
            GameObject loot = _factory.CreateLoot();
            loot.transform.position = this.transform.position;
        }
    }
}