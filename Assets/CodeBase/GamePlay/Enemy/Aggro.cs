using System.Collections;
using UnityEngine;

namespace CodeBase.GamePlay.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver observer;
        public Follow follow;

        public float chaseCoolDown = 3f;
        private Coroutine _agroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            observer.TriggerEnter += TriggerEnter;
            observer.TriggerExit += TriggerExit;
            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAggroCoroutine();
                SwitchFollowOnn();
            }
            
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                _agroCoroutine = StartCoroutine(SwitchFollowOffAfterCoolDown());
            }
            
        }

        private void OnDestroy()
        {
            observer.TriggerEnter -= TriggerEnter;
            observer.TriggerExit -= TriggerExit;
        }

        private void SwitchFollowOnn() => 
            follow.enabled = true;

        private void SwitchFollowOff() => 
            follow.enabled = false;

        private IEnumerator SwitchFollowOffAfterCoolDown()
        {
            yield return new WaitForSeconds(chaseCoolDown);
            SwitchFollowOff();
        }

        private void StopAggroCoroutine()
        {
            if (_agroCoroutine == null)
                return;
            StopCoroutine(_agroCoroutine);
            _agroCoroutine = null;
        }
    }
}