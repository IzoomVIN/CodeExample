using Services.Pool.Control;
using Services.Pool.Realization;
using UnityEngine;

namespace Shoot
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PoolableShell : Poolable
    {
        
        [Header("Object properties")]
        [SerializeField] internal float damage;
        [SerializeField] private float lifeTime;
        [SerializeField] private Renderer prefabRenderer;
        
        [Header("Effect properties")]
        [SerializeField] private PoolData effectPrefab;

        protected PoolOfGameObject EffectPool;
        
        private float _speed;
        
        private float _currentLifeTime;
        internal Rigidbody Rigidbody;

        internal void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            
            var poolManager = PoolManager.GetInstance();
            EffectPool = poolManager.AddPoolIfNotExist(
                effectPrefab.Prefab, 
                effectPrefab.StartCount, 
                effectPrefab.MaxCount
            );
        }

        private void LateUpdate()
        {
            _currentLifeTime -= Time.fixedDeltaTime;
            if (!prefabRenderer.isVisible && _currentLifeTime <= 0) Release();
        }

        internal void OnEnable()
        {
            _currentLifeTime = lifeTime;
            Rigidbody.velocity = Rigidbody.transform.forward * _speed;
        }
        
        public override void Clean()
        {
            _speed = 0;
        }

        public void Init(float speed)
        {
            _speed = speed;
        }
    }
}