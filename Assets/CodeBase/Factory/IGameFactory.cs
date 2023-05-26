using System.Collections.Generic;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHUD();
        void CleanUp();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
    }
}