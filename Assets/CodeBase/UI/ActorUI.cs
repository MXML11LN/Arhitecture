using CodeBase.GamePlay.Hero;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar hpBar;
        private HeroHealth _heroHealth;

        private void OnDestroy() => 
            _heroHealth.HealthChangedEvent -= UpdateHpBar;

        public void Construct(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChangedEvent += UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            
            Debug.Log($"current{_heroHealth.CurrentHp},max{_heroHealth.MaxHp}");
            hpBar.SetValue(_heroHealth.CurrentHp, _heroHealth.MaxHp);
        }
    }
}