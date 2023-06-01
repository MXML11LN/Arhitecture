using System;
using CodeBase.Data.Progress;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.GamePlay.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        private HeroState _heroState;
        public HeroAnimator heroAnimator;
        public event Action HealthChangerd;

        public float CurrentHP
        {
            get => _heroState.CurrentHp;
            set
            {
                if (_heroState.CurrentHp != value)
                {
                    _heroState.CurrentHp = value;
                    HealthChangerd?.Invoke();
                }
                
            }
        }

        public float MaxHP
        {
            get => _heroState.MaxHp;
            set
            {
                _heroState.MaxHp = value;
                HealthChangerd?.Invoke();
            }
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHP <= 0)
                return;

            CurrentHP -= damage;
            heroAnimator.PlayHit();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _heroState = progress.HeroState;
            HealthChangerd?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHp = CurrentHP;
            progress.HeroState.MaxHp = MaxHP;
        }
    }
}