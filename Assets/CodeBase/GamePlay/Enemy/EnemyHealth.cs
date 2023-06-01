using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{
    public class EnemyHealth : MonoBehaviour,IHealth
    {
        public event Action HealthChangedEvent;
        public float CurrentHp { get; set; }
        public float MaxHp { get; set; }
        public void TakeDamage(float damage)
        {
           
        }
    }
}