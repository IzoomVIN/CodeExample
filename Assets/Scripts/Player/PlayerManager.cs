using Services;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Player properties")]
        [SerializeField] private float tiltAnge = 30;
        [SerializeField] private float speed = 400;

        public float Speed => speed;
        public float TiltAnge => tiltAnge;
        public float XAxis { private set; get; }
        public float ZAxis { private set; get; }
        public GameManager GameManager { get; private set; }

        private void Awake()
        {
            GameManager = GameManager.GetInstance();
        }

        private void Update()
        {
            XAxis = Input.GetAxis("Horizontal");
            ZAxis = Input.GetAxis("Vertical");
        }
    }
}