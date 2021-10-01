using Effect;
using ObjectLife;
using UnityEngine;

namespace Shoot
{
    public class Laser : PoolableShell
    {

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<HpManager>()?.SetDamage(damage);

            

            var pos = other.ClosestPoint(transform.position);
            var speed = other.GetComponent<Rigidbody>().velocity;
            InitEffect(pos, speed);
            
            Release();
        }
        
        
        
        private void InitEffect(Vector3 position, Vector3 speed)
        {
            var effect = EffectPool.AcquireReusable();
            effect.GetComponent<EffectController>().Init(position, speed);
            
            effect.SetActive(true);
        }
    }
}