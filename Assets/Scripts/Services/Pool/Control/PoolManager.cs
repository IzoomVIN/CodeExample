using System;
using System.Collections.Generic;
using Services.Pool.Realization;
using UnityEngine;

namespace Services.Pool.Control
{
    public class PoolManager
    {
        private Dictionary<String, PoolOfGameObject> _poolMap;
        private static PoolManager _instance;

        private PoolManager()
        {
            _poolMap = new Dictionary<String, PoolOfGameObject>();
        }

        public static PoolManager GetInstance()
        {
            _instance ??= new PoolManager();
            return _instance;
        }

        public static void Destroy()
        {
            _instance = null;
        }
        
        public PoolOfGameObject AddPool(GameObject prefab, int startCount, int maxCount)
        {
            var pool = new PoolOfGameObject(prefab, startCount, maxCount);
            _poolMap.Add(prefab.name, pool);
            return pool;
        }

        public PoolOfGameObject AddPoolIfNotExist(GameObject prefab, int startCount, int maxCount)
        {
            if (_poolMap.ContainsKey(prefab.name)) return _poolMap[prefab.name];
            
            var pool = new PoolOfGameObject(prefab, startCount, maxCount);
            _poolMap.Add(prefab.name, pool);

            return pool;
        }
    }
}