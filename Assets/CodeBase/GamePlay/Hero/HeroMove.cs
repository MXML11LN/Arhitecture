using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay.Hero
{
    public class HeroMove : MonoBehaviour
    {
        public CharacterController controller;
        public float movementSpeed;
        private IInputService _inputService;
        private Camera _camera;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            CameraFollow();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            controller.Move(movementSpeed * movementVector * Time.deltaTime);
        }

        private void CameraFollow() => 
            _camera.GetComponent<CameraFollow>().Follow(this.gameObject);
    }
}