using UnityEngine;

namespace ObjectLife
{
    public class AsteroidHpManager : HpManager
    {
        
        [Header("Asteroid progress properties")]
        [SerializeField] private int pointCount;

        public delegate void AddProgress(int value);
        
        private event AddProgress ChangeProgress;
        protected internal override void Dead()
        {
            base.Dead();
            ChangeProgress?.Invoke(pointCount);
        }
        
        public void AddDelegate(AddProgress dg)
        {
            ChangeProgress += dg;
        }
    }
}