using Shoot;
using UnityEngine;

namespace Spawns
{
    public class AsteroidSpawn : Spawn
    {
        [Header("Asteroid speed range")]
        [SerializeField] private float minPrefabSpeed;
        [SerializeField] private float maxPrefabSpeed;

        protected override GameObject GetPrefab()
        {
            var elementIndex = Random.Range(0, elements.Length);
            var pool = Pools[elementIndex];
            return pool.AcquireReusable();
        }

        protected override void InitPrefab(GameObject prefab)
        {
            var position = transform.position;
            position.x = Random.Range(Manager.GameData.Boards.XLow, Manager.GameData.Boards.XUp);
                
            prefab.transform.position = position;
            prefab.transform.rotation = transform.rotation;
            
            prefab.GetComponent<Asteroid>().Init(Random.Range(minPrefabSpeed, maxPrefabSpeed));
            
            prefab.SetActive(true);
        }
    }
}