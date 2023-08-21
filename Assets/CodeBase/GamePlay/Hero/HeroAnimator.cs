using System;
using CodeBase.Logic.Animator;
using UnityEngine;

namespace CodeBase.GamePlay.Hero
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] public Animator _animator;

        private static readonly int MoveHash = Animator.StringToHash("Velocity");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int AttackStrong = Animator.StringToHash("AttackStrong");
        private static readonly int HitHash = Animator.StringToHash("Damage");
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int BlockHash = Animator.StringToHash("Block");
        private static readonly int JumpHash = Animator.StringToHash("Jump");

        private readonly int _idleStateFullHash = Animator.StringToHash("Base Layer.idle");
        private readonly int _attackStateHash = Animator.StringToHash("slash");
        private readonly int _strongAttackStateHash = Animator.StringToHash("attack");
        private readonly int _blockStateHash = Animator.StringToHash("Block");
        private readonly int _walkingStateHash = Animator.StringToHash("Moving");
        private readonly int _deathStateHash = Animator.StringToHash("death");
        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _jumpStateHash = Animator.StringToHash("Jump");


        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;
        public bool IsBlocking => State == AnimatorState.Blocking;
        public bool IsJumping => State == AnimatorState.Jumping;

        private void Update()
        {
            _animator.SetFloat(MoveHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }

        public void PlayHit() =>
            _animator.SetTrigger(HitHash);

        public void PlayJump() =>
            _animator.SetTrigger(JumpHash);
        public void PlayAttack() =>
            _animator.SetTrigger(AttackHash);

        public void PlayComboAttack() =>
            _animator.SetTrigger(AttackStrong);

        public void PlayBlock(bool blockStatus) =>
            _animator.SetBool(BlockHash,blockStatus);

        public void PlayDeath() =>
            _animator.SetTrigger(DieHash);

        public void ResetToIdle() =>
            _animator.Play(_idleStateHash, -1);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == _attackStateHash || stateHash == _strongAttackStateHash)
            {
                state = AnimatorState.Attack;
            }
            else if (stateHash == _walkingStateHash)
            {
                state = AnimatorState.Walking;
            }
            else if (stateHash == _deathStateHash)
            {
                state = AnimatorState.Died;
            }
            else if (stateHash == _blockStateHash)
            {
                state = AnimatorState.Blocking;
            }
            else if (stateHash == _jumpStateHash)
            {
                state = AnimatorState.Jumping;
            }
            else
            {
                state = AnimatorState.Unknown;
            }
            return state;
        }
    }
}