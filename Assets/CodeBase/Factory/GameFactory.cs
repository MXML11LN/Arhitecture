using CodeBase.AssetManagement;
using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        
        [Inject]
        public GameFactory(IAssetProvider assetProvider)
        {
            _assets = assetProvider;
        }

        public GameObject CreateHero(GameObject at) =>
            _assets
                .Instantiate(AssetPath.HeroPrefabPath, at.transform.position);

        public GameObject CreateHUD() => 
            _assets
                .Instantiate(AssetPath.HUDPrefabPath);
    }
}