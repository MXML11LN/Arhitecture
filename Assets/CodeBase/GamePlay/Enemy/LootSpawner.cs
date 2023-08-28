using System;
using CodeBase.Data;
using CodeBase.Factory;
using CodeBase.Services.Random;
using TMPro;
using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{ 
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath enemyDeath;
        private IGameFactory _factory;
        private int _lotMin;
        private int _lootMax;
        private IRandomService _randomService;

        public void Construct(IGameFactory factory,IRandomService randomService)
        {
            _factory = factory;
            _randomService = randomService;
        }

        private void Start() => 
            enemyDeath.Happened += SpawnLoot;

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = this.transform.position;
            Loot lootItem = GenerateLoot();
            loot.Initialize(lootItem);
        }

        private Loot GenerateLoot() =>
            new Loot
            {
                value = _randomService.Next(_lotMin,_lootMax)
            };

        public void SetLoot(int min, int max)
        {
            _lotMin = min;
            _lootMax = max;
        }
    }
}