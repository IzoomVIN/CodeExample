using System;
using Services.Pool.Realization;
using UnityEngine;

namespace Services.Pool.Control
{
    public abstract class PoolController : MonoBehaviour
    {
        [Header("Pool properties")] 
        [SerializeField] protected PoolData[] elements;

        protected PoolOfGameObject[] Pools;
        
        private void Awake()
        {
            if (elements.Length == 0)
            {
                throw new Exception($"Array of prefabs is empty in {name}");
            }
            
            InitPools();
        }
        
        private void InitPools()
        {
            var poolManager = PoolManager.GetInstance();
            Pools = new PoolOfGameObject[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                var pool = poolManager.AddPool(elements[i].Prefab, elements[i].StartCount, elements[i].MaxCount);
                Pools[i] = pool;
            }
        }
    }
}