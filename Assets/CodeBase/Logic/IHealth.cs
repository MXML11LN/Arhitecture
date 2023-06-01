using System;

namespace CodeBase.Logic
{
    public interface IHealth
    {
        event Action HealthChangedEvent;
        float CurrentHp { get; set; }
        float MaxHp { get; set; }
        void TakeDamage(float damage);
    }
}