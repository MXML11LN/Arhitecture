using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public event Action HealthChangedEvent;

        [SerializeField] private float _currentHp;
        [SerializeField] private float _maxHp;

        public float CurrentHp
        {
            get => _currentHp;
            set => _currentHp = value;
        }

        public float MaxHp
        {
            get => _maxHp;
            set => _maxHp = value;
        }

        public EnemyAnimator enemyAnimator;

        public void TakeDamage(float damage)
        {
            CurrentHp -= damage;
            enemyAnimator.PlayDamage();
            HealthChangedEvent?.Invoke();
        }
    }
}