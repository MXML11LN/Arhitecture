using UnityEngine;

namespace CodeBase.Services.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/MonsterData",fileName = "MonsterData")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId TypeId;
        [Range(1,10)]
        public int maxLoot = 1;
        [Range(10,100)]
        public int minLoot=10;
        
        [Range(1,1000)]
        public int Hp;
        
        [Range(1,100)]
        public int Damage = 20;
        
        [Range(1f,10f)]
        public float AttackCoolDown = 3f;
        
        [Range(.5f,3f)]
        public float Clevage = 3f;
        
        [Range(.5f,5f)]
        public float EffectiveDistance = 2f;

        [Range(1f,10f)]
        public float moveSpeed = 5f;

        public GameObject MonsterPrefab;
    }
}