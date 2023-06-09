﻿using System;
using System.Collections.Generic;
using CodeBase.AssetManagement;
using CodeBase.GamePlay.Hero;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject Hero { get; set;}
        public event Action HeroCreated;

        [Inject]
        public GameFactory(IAssetProvider assetProvider)
        {
            _assets = assetProvider;
        }

        public GameObject CreateHero(GameObject at)
        {
            Hero = InstantiateRegistered(AssetPath.HeroPrefabPath, at.transform.position);
            HeroCreated?.Invoke();
            return Hero;
        }

        public GameObject CreateHUD() => 
            InstantiateRegistered(AssetPath.HUDPrefabPath);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
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