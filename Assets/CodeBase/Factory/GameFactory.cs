using System.Collections.Generic;
using CodeBase.AssetManagement;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        [Inject]
        public GameFactory(IAssetProvider assetProvider)
        {
            _assets = assetProvider;
        }

        public GameObject CreateHero(GameObject at) => 
            InstantiateRegistered(AssetPath.HeroPrefabPath, at.transform.position);

        public GameObject CreateHUD() =>
            InstantiateRegistered(AssetPath.HUDPrefabPath);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            RegisterProgressWatchers(_assets.Instantiate(prefabPath, at));
            return _assets.Instantiate(prefabPath, at);
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter) 
                ProgressWriters.Add(progressWriter);
            ProgressReaders.Add(progressReader);
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}