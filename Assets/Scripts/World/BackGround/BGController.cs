using UnityEngine;

namespace World.BackGround
{
    public class BGController : MonoBehaviour
    {
        [SerializeField] private BackGround[] prefabs;
        [SerializeField] private Transform startPoint;
        [SerializeField] private float speed;

        private BackGround _current;
        private BackGround _previous;
        private BackGround _next;
        private int _nextIndex;

        private void Awake()
        {
            _nextIndex = 0;
            foreach (var element in prefabs)
            {
                element.gameObject.SetActive(false);
            }

            _current = GetNextElement(startPoint);
            _next = GetNextElement(_current.NextPoint);
        }

        private void LateUpdate()
        {
            if (_next != null
                && _next.Renderer.isVisible)
                ChangeCurrentElement();
            
            if (_previous != null 
                && !_previous.Renderer.isVisible)
                RemovePrevElement();
        }

        private void FixedUpdate()
        {
            if (_next == null) _next = GetNextElement(_current.NextPoint);
            
            Move();
        }

        private void Move()
        {
            _current.transform.Translate(0, 0, -speed);
            _next.transform.Translate(0, 0, -speed);
            if (_previous != null) 
                _previous.transform.Translate(0, 0, -speed);
        }

        private BackGround GetNextElement(Transform currentElement)
        {
            var next = prefabs[_nextIndex];
            next.gameObject.SetActive(true);

            var newPosition = currentElement.position;
            newPosition -= (next.PrevPoint.position - next.transform.position);
            
            next.transform.position = newPosition;

            _nextIndex++;
            _nextIndex %= prefabs.Length;

            return next;
        }

        private void ChangeCurrentElement()
        {
            _previous = _current;
            _current = _next;
            _next = null;
        }

        private void RemovePrevElement()
        {
            _previous.gameObject.SetActive(false);
            _previous = null;
        }
    }
}