using System;
using UnityEngine;

namespace Services.Pool.Control
{
    [Serializable]
    public struct PoolData
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int startCount;
        [SerializeField] private int maxCount;

        public GameObject Prefab => prefab;
        public int StartCount => startCount;
        public int MaxCount => maxCount;
            
        public PoolData(GameObject prefab, int startCount, int maxCount)
        {
            this.prefab = prefab;
            this.startCount = startCount;
            this.maxCount = maxCount;
        }
    }
}