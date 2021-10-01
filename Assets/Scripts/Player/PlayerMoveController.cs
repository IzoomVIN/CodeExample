using Services.Models;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private PlayerManager manager;
        [SerializeField] private ParticleSystem[] turbineFires;
        [SerializeField] private float turbineFireSpeed = 1.6f;
        
        private Rigidbody _rigidbody;
        private GameData.WorldBoards _boards;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _boards = manager.GameManager.GameData.Boards;
        }

        private void FixedUpdate()
        {
            MovementLogic();
            TiltLogic();
            TurbineSpeedScale();
        }
    
        private void MovementLogic()
        {
            var vel = new Vector3(manager.XAxis, 0, manager.ZAxis) * manager.Speed;
            _rigidbody.velocity = vel;
    
            var pos = _rigidbody.position;
            var clampPos = new Vector3(
                Mathf.Clamp(pos.x, _boards.XLow, _boards.XUp), 
                    pos.y, 
                Mathf.Clamp(pos.z, _boards.ZLow, _boards.ZUp)
                );
            
            _rigidbody.position = clampPos;
        }
    
        private void TiltLogic()
        {
            if (manager.XAxis == 0) return;
    
            var tilt = manager.TiltAnge * manager.XAxis;
            _rigidbody.rotation = Quaternion.Euler(0,0,-tilt);
        }
    
        private void TurbineSpeedScale()
        {
            if (manager.ZAxis == 0) return;
            
            foreach (var particle in turbineFires)
            {
                var multiplier = turbineFireSpeed;
                multiplier += Mathf.Clamp(manager.ZAxis, -1, 1);
                
                var velocityOverLifetime = particle.velocityOverLifetime;
                velocityOverLifetime.speedModifier = multiplier;
            }
        }
    }
}