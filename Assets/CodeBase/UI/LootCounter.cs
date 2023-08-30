using CodeBase.Data.Progress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI coinsText;
        private WorldData _worldData;


        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += UpdateCoins;
        }

        private void Start()
        {
            UpdateCoins();
        }

        private void OnDestroy()
        {
            _worldData.LootData.Changed -= UpdateCoins;        }

        private void UpdateCoins()
        {
            coinsText.text = $"{_worldData.LootData.collected}";
        }
    }
}