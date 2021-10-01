using Services.Pool.Control;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.Pool.Realization
{
    public class PoolOfGameObject
    {
        private readonly ObjectPool<GameObject> _pool;
        public int MaxSize { get; private set; }
        public int Count => _pool.Count;

        public PoolOfGameObject(GameObject prefab, int startCount, int maxSize)
        {
            var creatorOfGameObject = new CreatorOfGameObject(prefab, this);
            MaxSize = maxSize;
            _pool = new ObjectPool<GameObject>(creatorOfGameObject, startCount, maxSize);
        }

        public GameObject AcquireReusable()
        {
            return _pool.GetElement();
        }

        public void ReleaseReusable(GameObject reusable)
        {
            reusable.SetActive(false);
            _pool.SetElement(reusable);
        }

        private class CreatorOfGameObject : ObjectPool<GameObject>.ICreation<GameObject>
        {
            private readonly GameObject _prefab;
            private readonly PoolOfGameObject _poolRef;

            internal CreatorOfGameObject(GameObject prefabs, PoolOfGameObject poolRef)
            {
                _prefab = prefabs;
                _poolRef = poolRef;
            }

            public GameObject Create()
            {
                var element = Object.Instantiate(_prefab);
                element.name = _prefab.name;
                element.GetComponent<Poolable>().SetPool(_poolRef);
                element.SetActive(false);

                return element;
            }
        }
    }
}