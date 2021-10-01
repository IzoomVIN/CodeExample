using Effect;
using ObjectLife;
using Services;
using UnityEngine;
using World;
using Random = UnityEngine.Random;

namespace Shoot
{
    public class Asteroid : PoolableShell
    {
        [SerializeField] private float effectScale;
        
        [Header("Rotation properties")] 
        [SerializeField] private float minRotationSpeed;
        [SerializeField] private float maxRotationSpeed;

        private AsteroidHpManager _hpManager;

        private new void Awake()
        {
            base.Awake();
            _hpManager = GetComponent<AsteroidHpManager>();
            _hpManager.AddDelegate(GameManager.GetInstance().PlayerData.AddPoints);
        }

        private new void OnEnable()
        {
            base.OnEnable();
            var torque = Random.insideUnitSphere;
            torque *= Random.Range(minRotationSpeed, maxRotationSpeed);
            Rigidbody.AddTorque(torque);
        }

        private void OnCollisionEnter(Collision other)
        {
            
            if (other.transform.CompareTag("Player"))
            {
                other.transform.GetComponent<HpManager>()?.SetDamage(damage);
                _hpManager.Dead();
            }
        }

        public void DeadHandle()
        {
            InitEffect(Rigidbody.position, Rigidbody.velocity, effectScale);
            Release();
        }

        private void InitEffect(Vector3 position, Vector3 speed, float scale)
        {
            var effect = EffectPool.AcquireReusable();
            effect.GetComponent<EffectController>().Init(position, speed, scale);
            
            effect.SetActive(true);
        }
    }
}