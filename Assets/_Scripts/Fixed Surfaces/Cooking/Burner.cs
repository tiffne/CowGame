using UnityEngine;

namespace _Scripts.Fixed_Surfaces.Cooking
{
    public class Burner : Surface
    {
        private CircleCollider2D circleCollider2D;
        private new void Start()
        {
            base.Start();
            circleCollider2D = GetComponent<CircleCollider2D>();
        }
        private void Update()
        {
            circleCollider2D.enabled = transform.childCount == 0;
        }
    }
}
