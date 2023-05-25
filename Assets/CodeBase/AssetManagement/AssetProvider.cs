using CodeBase.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly DiContainer _container;

        [Inject]
        public AssetProvider(DiContainer container)
        {
            _container = container;
        }

        public GameObject Instantiate(string prefabPath)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            return _container.InstantiatePrefab(prefab);
        }

        public GameObject Instantiate(string prefabPath, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            return _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
        }
    }
}