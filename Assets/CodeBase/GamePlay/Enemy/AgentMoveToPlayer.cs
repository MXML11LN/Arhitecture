using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.GamePlay.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        public NavMeshAgent agent;
        public Transform heroTransform;
        public EnemyDeath Death;

        public void Construct(Transform heroGameObject) =>
            heroTransform = heroGameObject;

        private void Update() => SetDestinationForAgent();

        private void SetDestinationForAgent()
        {
            if (heroTransform != null && IsHeroReached() && !Death.IsDead)
                agent.destination = heroTransform.position;
        }

        private bool IsHeroReached() =>
            Vector3.Distance(agent.transform.position, heroTransform.position) >= agent.stoppingDistance;
    }
}