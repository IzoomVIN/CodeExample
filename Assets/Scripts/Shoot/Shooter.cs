using System.Collections;
using System.Linq;
using Services.Pool.Control;
using UnityEngine;

namespace Shoot
{
    public class Shooter : PoolController
    {
        [SerializeField] private float recharge;
        
        [Header("Bullet properties")]
        [SerializeField] private Transform startPosition;
        [SerializeField] private float speed;

        private void Start()
        {
            StartCoroutine(nameof(Shoot));
        }

        public IEnumerator Shoot()
        {
            while (true)
            {
                var bullet = Pools.First().AcquireReusable();
                bullet.transform.position = startPosition.position;
                bullet.transform.rotation = startPosition.rotation;
                bullet.GetComponent<PoolableShell>().Init(speed);
            
                bullet.SetActive(true);
            
                yield return new WaitForSeconds(recharge);
            }
        }
    }
}