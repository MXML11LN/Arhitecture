using System;
using CodeBase.Data.Progress;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.GamePlay.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour,IHealth, ISavedProgress
    {
        private HeroState _heroState;
        public HeroAnimator heroAnimator;
        public event Action HealthChangedEvent;

        public float CurrentHp
        {   
            get => _heroState.CurrentHp;
            set
            {
                if (_heroState.CurrentHp != value)
                {
                    _heroState.CurrentHp = value;
                    HealthChangedEvent?.Invoke();
                }
                
            }
        }

        public float MaxHp
        {
            get => _heroState.MaxHp;
            set
            {
                _heroState.MaxHp = value;
                HealthChangedEvent?.Invoke();
            }
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHp <= 0)
                return;

            CurrentHp -= damage;
            heroAnimator.PlayHit();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _heroState = progress.HeroState;
            HealthChangedEvent?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHp = CurrentHp;
            progress.HeroState.MaxHp = MaxHp;
        }
    }
}