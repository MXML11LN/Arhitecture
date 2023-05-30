using System;
using CodeBase.Logic.Animator;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int WinHash = Animator.StringToHash("Win");
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int DamageHash = Animator.StringToHash("Damage");
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int Attack1Hash = Animator.StringToHash("Attack1");
        private static readonly int Attack2Hash = Animator.StringToHash("Attack2");

        private static int _idleStateHash = Animator.StringToHash("idle");
        private static int _attackStateHash = Animator.StringToHash("attack1");
        private static int _walkStateHash = Animator.StringToHash("Move");
        private static int _deathStateHash = Animator.StringToHash("death");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        private Animator _animator;


        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayDeath() => _animator.SetTrigger(DieHash);
        public void PlayWin() => _animator.SetTrigger(WinHash);
        public void PlayAttack1() => _animator.SetTrigger(Attack1Hash);
        public void PlayAttack2() => _animator.SetTrigger(Attack2Hash);
        public void PlayDamage() => _animator.SetTrigger(DamageHash);
        public void StopMoving() => _animator.SetBool(IsMovingHash, false);

        public void Move(float speed)
        {
            _animator.SetBool(IsMovingHash, true);
            _animator.SetFloat(SpeedHash, speed);
        }


        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
            return state;
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(StateFor(stateHash));
    }
}