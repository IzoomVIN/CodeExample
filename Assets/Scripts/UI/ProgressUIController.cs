using Services;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ProgressUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countView;

        public void UpdateCount(int value)
        {
            countView.text = value.ToString();
        }

        private void OnEnable()
        {
            countView.text = GameManager.GetInstance().PlayerData.Points.ToString();
        }
    }
}