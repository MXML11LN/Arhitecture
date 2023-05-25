using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _container;

        [Inject]
        public GameFactory(DiContainer container)
        {
            _container = container;
        }

        public const string HeroPrefabPath = "Prefabs/Hero";
        public const string HUDPrefabPath = "Prefabs/HUD";

        public GameObject CreateHero(GameObject at) => 
            Instantiate(HeroPrefabPath, at.transform.position);
        public GameObject CreateHUD() => 
            Instantiate(HUDPrefabPath);

        private GameObject Instantiate(string prefabPath)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            return _container.InstantiatePrefab(prefab);
        }

        private GameObject Instantiate(string prefabPath,Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            return _container.InstantiatePrefab(prefab,at,Quaternion.identity,null);
        }
    }
}