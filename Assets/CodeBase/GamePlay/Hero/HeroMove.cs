using CodeBase.Data.Progress;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.GamePlay.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        public CharacterController controller;
        public float movementSpeed;
        private IInputService _inputService;
        public HeroAnimator Animator;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon && !Animator.IsAttacking)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            controller.Move(movementSpeed * movementVector * Time.deltaTime);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(LevelName(),transform.position.AsVector3Data());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (LevelName() == progress.WorldData.PositionOnLevel.LevelName)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition !=  null) 
                    Warp(to: savedPosition);
            }

        }

        private void Warp(Vector3Data to)
        {
            controller.enabled = false;
            transform.position = to.AsUnityVector3().AddY(controller.height/2);
            controller.enabled = true;
        }

        private static string LevelName() => 
            SceneManager.GetActiveScene().name;
    }
}