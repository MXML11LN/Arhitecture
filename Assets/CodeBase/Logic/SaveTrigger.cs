using CodeBase.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        public BoxCollider boxCollider;

        [Inject]
        public void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _saveLoadService.SaveProgress();
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (boxCollider)
                return;
            Gizmos.color = new Color(30, 200, 30, 100);
            Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }
}