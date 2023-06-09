using System;
using CodeBase.Data.Progress;
using CodeBase.Logic;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public HeroAnimator heroAnimator;
        public CharacterController characterController;

        private IInputService _inputService;
        private static int _layerMask;
        private readonly Collider[] _hits = new Collider[3];
        private HeroStats _stats;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        void Update()
        {
            if (_inputService.IsAttackButtonPressed() && !heroAnimator.IsAttacking)
            {
                heroAnimator.PlayAttack();
            }
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
               _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage); 
            }
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.AttackRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, characterController.center.y / 2f, transform.position.z);

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }
    }
}