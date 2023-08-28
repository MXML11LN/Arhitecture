using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Progress;
using TMPro;
using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        
        public GameObject Coin;
        public GameObject pickupFxPrefab;
        public TextMeshProUGUI pickupText;
        public GameObject pickUpPopUp;
        
        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

       
        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }
        
        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) => 
            PickUp();

        private void PickUp()
        {
            if(_picked) 
                return;
            _picked = true;
            UpdateLoot();
            HideCoin();
            PlayPickUpFx();
            ShowText();
            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateLoot() => _worldData.LootData.Collect(_loot);

        private void HideCoin() => 
            Coin.SetActive(false);

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }

        private void PlayPickUpFx() => 
            Instantiate(pickupFxPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            pickupText.text = $"{_loot.value}";
            pickUpPopUp.gameObject.SetActive(true);
        }
    }
}