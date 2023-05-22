using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string HorizontalAxisName = "Horizontal";
        protected const string VerticalAxisName = "Vertical";
        private const string AttackButtonName = "Fire";

        public abstract Vector2 Axis { get; } 
        
        public bool IsAttackButtonPressed() =>
            SimpleInput.GetButtonUp(AttackButtonName);

        protected static Vector2 SimpleInputAxis() => 
            new Vector2(SimpleInput.GetAxis(HorizontalAxisName), SimpleInput.GetAxis(VerticalAxisName));
    }
}