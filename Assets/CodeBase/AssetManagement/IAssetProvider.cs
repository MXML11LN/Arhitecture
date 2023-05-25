using UnityEngine;

namespace CodeBase.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string prefabPath);
        GameObject Instantiate(string prefabPath, Vector3 at);
    }
}