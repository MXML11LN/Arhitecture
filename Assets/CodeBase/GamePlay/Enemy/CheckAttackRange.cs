using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        public Attack attack;

        public TriggerObserver triggerObserver;

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;

            attack.DisableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            attack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            attack.EnableAttack();
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= TriggerEnter;
            triggerObserver.TriggerExit -= TriggerExit;
        }
    }
}