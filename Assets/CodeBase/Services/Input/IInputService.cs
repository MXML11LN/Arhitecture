using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService
    {
        abstract Vector2 Axis { get; }
        bool IsAttackButtonPressed();
    }
}