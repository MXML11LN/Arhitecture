using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        abstract Vector2 Axis { get; }
        bool IsAttackButtonPressed();
    }
}