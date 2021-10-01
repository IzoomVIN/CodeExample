using UnityEngine;
using UnityEngine.Events;

namespace ObjectLife
{
    public class HpManager : MonoBehaviour
    {
        [SerializeField] private float healthPoints;
        [SerializeField] private UnityEvent isDead;
        [SerializeField] private UnityEvent<float> hpChange;

        private float _currentHP;
        
        public void SetDamage(float damage)
        {
            _currentHP = damage > _currentHP ? 0 : _currentHP - damage;

            hpChange.Invoke(_currentHP/healthPoints);
            
            if (_currentHP == 0)
            {
                Dead();
            }
        }

        protected internal virtual void Dead()
        {
            isDead.Invoke();
        }

        private void OnEnable()
        {
            _currentHP = healthPoints;
        }
    }
}