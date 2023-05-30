using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent), typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        public NavMeshAgent agent;
        public EnemyAnimator enemyAnimator;
        public float minimalVelocity = 0.5f;

        private void Update()
        {
            if (ShouldMove())
                enemyAnimator.Move(agent.velocity.magnitude);
            else
            {
                enemyAnimator.StopMoving();
            }
        }

        private bool ShouldMove() =>
            agent.velocity.magnitude >= minimalVelocity && agent.remainingDistance>= agent.stoppingDistance;
    }
}