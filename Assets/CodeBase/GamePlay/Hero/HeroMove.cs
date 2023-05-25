using CodeBase.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.GamePlay.Hero
{
    public class HeroMove : MonoBehaviour
    {
        public CharacterController controller;
        public float movementSpeed;
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            controller.Move(movementSpeed * movementVector * Time.deltaTime);
        }
    }
}