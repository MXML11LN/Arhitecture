using CodeBase.Factory;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.GamePlay.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        public NavMeshAgent agent;
        public Transform heroTransform;
        private IGameFactory _gameFactory;
        public EnemyDeath Death;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Start()
        {
            if(_gameFactory.Hero!=null)
                InitHeroTransform();
            _gameFactory.HeroCreated += InitHeroTransform;

        }

        private void Update()
        {
            if(heroTransform!=null && IsHeroReached() && !Death.IsDead)
                agent.destination = heroTransform.position;
        }

        private bool IsHeroReached() => 
            Vector3.Distance(agent.transform.position, heroTransform.position)>=agent.stoppingDistance;

        private void InitHeroTransform() => heroTransform = _gameFactory.Hero.transform;

        private void OnDestroy()
        {
            _gameFactory.HeroCreated -= InitHeroTransform;
        }
    }
}