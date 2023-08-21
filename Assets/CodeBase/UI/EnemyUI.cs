using CodeBase.GamePlay.Enemy;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
    public class EnemyUI : MonoBehaviour
    {
        public HpBar hpBar;
        private IHealth enemyHealth;

        public void Construct(IHealth health)
        {
            enemyHealth = health;
        }
        
        private void Start()
        {
            enemyHealth.HealthChangedEvent += UpdateHpBar;
        }

        private void OnDestroy() => 
            enemyHealth.HealthChangedEvent -= UpdateHpBar;
        

        private void UpdateHpBar()
        {
            Debug.Log($"current{enemyHealth.CurrentHp},max{enemyHealth.MaxHp}");
            hpBar.SetValue(enemyHealth.CurrentHp, enemyHealth.MaxHp);
        }
    }
}