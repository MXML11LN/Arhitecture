using UnityEngine;

namespace CodeBase.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHUD();
    }
}