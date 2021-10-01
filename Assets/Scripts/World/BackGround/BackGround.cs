using UnityEngine;

namespace World.BackGround
{
    public class BackGround : MonoBehaviour
    {
    
        public Transform NextPoint { private set; get; }
        public Transform PrevPoint { private set; get; }
        public Renderer Renderer { private set; get; }

        public void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.name.Equals("NextPoint")) NextPoint = child;
                if (child.name.Equals("PrevPoint")) PrevPoint = child;
            }

            Renderer = GetComponent<Renderer>();
        }
    }
}