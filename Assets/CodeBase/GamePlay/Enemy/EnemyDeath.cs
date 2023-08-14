using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{[RequireComponent(typeof(EnemyHealth),typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth health;
        public EnemyAnimator enemyAnimator;
        public Follow enemyFollow;
        public GameObject DeathFxPrefab;
        [SerializeField]private float deathTime = 3f;
        public event Action Happened;
        public bool IsDead;

        private void Start() => 
            health.HealthChangedEvent+= HealthChangedEvent;

        private void OnDestroy()
        {
            health.HealthChangedEvent -= HealthChangedEvent;
        }

        private void HealthChangedEvent()
        {
            if (health.CurrentHp <= 0f) 
                Die();
        }

        private void Die()
        {
            IsDead = true;
            health.HealthChangedEvent -= HealthChangedEvent;
            enemyAnimator.PlayDeath();
            enemyFollow.enabled = false;
            StartCoroutine(DestroyTimer());
            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(deathTime);
            SpawnDestroyFx();
            Destroy(gameObject);
        }

        private void SpawnDestroyFx() => 
            Instantiate(DeathFxPrefab, transform.position, Quaternion.identity);
    }
}