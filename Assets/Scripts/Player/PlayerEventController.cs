using Effect;
using Services;
using Services.Models;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerEventController : MonoBehaviour
    {
        [SerializeField] private GameObject deadEffect;

        public void Dead()
        {
            var effect = Instantiate(deadEffect);
            effect.SetActive(false);
            
            effect.GetComponent<EffectController>().Init(transform.position, Vector3.zero);
            effect.SetActive(true);
            
            FindObjectOfType<UIEvents>().GameOverAction();
            
            Destroy(gameObject);
        }

        public void HpChange(float value)
        {
            GameManager.GetInstance().PlayerData.ChangeHp(value);
        }
    }
}