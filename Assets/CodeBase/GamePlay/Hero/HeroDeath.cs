using System;
using CodeBase.Data.Progress;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.GamePlay.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth heroHealth;
        public CharacterController characterController;
        public HeroMove heroMove;
        public HeroAnimator heroAnimator;
        public GameObject deathFX;
        private bool _isDead;
        public HeroAttack heroAttack;

        private void Start()
        {
            heroHealth.HealthChangedEvent += HealthChanged;
        }

        private void OnDestroy()
        {
            heroHealth.HealthChangedEvent -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (!_isDead && heroHealth.CurrentHp <= 0)
                Die();

        }

        private void Die()
        {
            _isDead = true;
            heroMove.enabled = false;
            characterController.enabled = false;
            heroAttack.enabled = false;
            heroAnimator.PlayDeath();
            Instantiate(deathFX, transform.position,Quaternion.identity);
        }
        
    }
}