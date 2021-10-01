using System.Collections;
using Services;
using Services.Pool.Control;
using UnityEngine;
using World;
using Random = UnityEngine.Random;

namespace Spawns
{
    
    public abstract class Spawn : PoolController
    {
        [Header("Spawn delay range")]
        [SerializeField] private float minDelaySecSpawn;
        [SerializeField] private float maxDelaySecSpawn;
        
        internal GameManager Manager;

        private void Start()
        {
            Manager = GameManager.GetInstance();
            
            StartCoroutine(nameof(DoSpawn));
        }

        protected abstract GameObject GetPrefab();
        protected abstract void InitPrefab(GameObject prefab);
        
        private IEnumerator DoSpawn()
        {
            while (true)
            {
                var element = GetPrefab();
                InitPrefab(element);
            
                yield return new WaitForSeconds(Random.Range(minDelaySecSpawn, maxDelaySecSpawn));
            }
        }
    }
}