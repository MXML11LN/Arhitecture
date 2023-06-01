using CodeBase.GamePlay.Hero;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar hpBar;
        private HeroHealth _heroHealth;

        private void OnDestroy() => 
            _heroHealth.HealthChangerd -= UpdateHpBar;

        public void Construct(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChangerd += UpdateHpBar;
        }

        private void UpdateHpBar() => 
            hpBar.SetValue(_heroHealth.CurrentHP,_heroHealth.MaxHP);
    }
}