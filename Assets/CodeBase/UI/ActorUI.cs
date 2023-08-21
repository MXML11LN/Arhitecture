using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar hpBar;
        private IHealth _heroHealth;
        public void Construct(IHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChangedEvent += UpdateHpBar;
        }
        private void OnDestroy() => 
            _heroHealth.HealthChangedEvent -= UpdateHpBar;

        private void UpdateHpBar()
        {
            Debug.Log($"current{_heroHealth.CurrentHp},max{_heroHealth.MaxHp}");
            hpBar.SetValue(_heroHealth.CurrentHp, _heroHealth.MaxHp);
        }
    }
}