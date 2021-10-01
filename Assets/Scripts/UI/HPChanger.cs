using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HPChanger : MonoBehaviour
    {
        [SerializeField] private Image hp;
        [SerializeField] private Image hpCoroutine;
        [Header("Coroutine properties")]
        [SerializeField] private float amountStep;
        [SerializeField] private float delay;
        [SerializeField] private float delayBefore;
        
        private float _currentAmount;
        private bool _coroutineIsWork;

        private void Awake()
        {
            _currentAmount = 1;
        }

        /// <param name="value">value of change of HP. Must in range from 0 to 1</param>
        public void ChangeHp(float value)
        {
            // InputValue isn't in standard range
            if (value > 1 || value < 0) return;
            
            if (value > _currentAmount)
            {
                _currentAmount = value;
                hp.fillAmount = _currentAmount;
                hpCoroutine.fillAmount = _currentAmount;
            }
            else
            {
                _currentAmount = value;
                hp.fillAmount = _currentAmount;
                if(!_coroutineIsWork) StartCoroutine(nameof(Change));   
            }
        }

        private IEnumerator Change()
        {
            yield return new WaitForSeconds(delayBefore);
            while (hpCoroutine.fillAmount > _currentAmount)
            {
                hpCoroutine.fillAmount -= amountStep;
                if (hpCoroutine.fillAmount == 0)
                {
                    _coroutineIsWork = false;
                    yield break;
                }
                yield return new WaitForSeconds(delay);
            }
            yield break;
        }
    }
}