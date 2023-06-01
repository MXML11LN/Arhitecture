using System;
using System.Linq;
using CodeBase.Factory;
using CodeBase.Logic;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {

        public float AttackCoolDown = 3f;
        public float Clevage = 3f;
        public float EffectiveDistance = 2f;
        public int damage = 20;
        public GameObject attackFx;

        public EnemyAnimator enemyAnimator;
        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        
        private int _layerMask;
        private float _attackCoolDownRemaining;
        private bool _isAttacking;
        private bool _attackIsActive;
        private Collider[] _hits = new Collider[1];
        
        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _gameFactory.HeroCreated += OnHeroCreated;
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }


        public void DisableAttack() => 
            _attackIsActive = false;

        public void EnableAttack() => 
            _attackIsActive = true;

        private void Update()
        {
            UpdateCoolDown();
            if (CanAttack()) 
                StartAttack();
        }


        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(damage);
                Instantiate(attackFx, StartPoint(), Quaternion.identity);
            }
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawSphere(StartPoint(),Clevage);
        // }

        private void OnAttackCompleted()
        {
            _attackCoolDownRemaining = AttackCoolDown;
            _isAttacking = false;
        }

        private void UpdateCoolDown()
        {
            if (!CoolDownIsElapsed())
                _attackCoolDownRemaining -= Time.deltaTime;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            enemyAnimator.PlayAttack1();
            _isAttacking = true;
        }

        private bool CoolDownIsElapsed() => 
            _attackCoolDownRemaining < 0;

        private bool CanAttack() => _attackIsActive && !_isAttacking && CoolDownIsElapsed();

        private Vector3 StartPoint()
        {
            return transform.position.AddY(0.5f) + transform.forward * EffectiveDistance;
        }

        private void OnHeroCreated() => 
            _heroTransform = _gameFactory.Hero.transform;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Clevage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }
    }
}