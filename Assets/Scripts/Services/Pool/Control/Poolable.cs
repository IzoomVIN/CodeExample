using Services.Pool.Realization;
using UnityEngine;

namespace Services.Pool.Control
{
    public abstract class Poolable : MonoBehaviour, IPoolable
    {
        private PoolOfGameObject _pool;

        public void SetPool(PoolOfGameObject pool)
        {
            _pool = pool;
        }

        public void Release()
        {
            Clean();
            _pool.ReleaseReusable(gameObject);
        }

        public abstract void Clean();
    }
}