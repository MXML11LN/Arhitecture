using CodeBase.AssetManagement;
using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    public class GameFactory : IGameFactory
    {

        public const string HeroPrefabPath = "Prefabs/Hero";
        public const string HUDPrefabPath = "Prefabs/HUD";
        
        private readonly AssetProvider _assets;

        [Inject]
        public GameFactory(AssetProvider assetProvider)
        {
            _assets = assetProvider;
        }

        public GameObject CreateHero(GameObject at) => _assets.Instantiate(HeroPrefabPath,at.transform.position);

        public GameObject CreateHUD() => _assets.Instantiate(HUDPrefabPath);
    }
}