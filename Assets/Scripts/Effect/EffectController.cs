using Services.Pool.Control;
using UnityEngine;

namespace Effect
{
    public class EffectController : Poolable
    {
        [SerializeField] private ParticleSystem particleEffect;
        private Vector3 _position;
        private Vector3 _speed;
        private float _scaleMult;
        private Vector3 _standardScale;


        private void Awake()
        {
            _position = Vector3.zero;
            _speed = Vector3.zero;
            _standardScale = transform.localScale;
        }

        private void FixedUpdate()
        {
            transform.Translate(_speed*Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            if(!particleEffect.isPlaying) Release();
        }

        private void OnEnable()
        {
            transform.position = _position;
            transform.localScale = _standardScale * _scaleMult;
            particleEffect.Play();
        }

        public override void Clean()
        {
            _position = Vector3.zero;
            _speed = Vector3.zero;
            _scaleMult = 1;
        }

        public void Init(Vector3 position, Vector3 speed, float scale = 1)
        {
            _scaleMult = scale;
            _position = position;
            _speed = speed;
        }
    }
}