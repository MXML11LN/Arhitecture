using CodeBase.GamePlay.Enemy;
using UnityEngine;

namespace CodeBase.UI
{
    public class EnemyUI : MonoBehaviour
    {
        public HpBar hpBar;
        public EnemyHealth enemyHealth;

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