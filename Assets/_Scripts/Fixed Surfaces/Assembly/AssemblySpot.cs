using UnityEngine;

namespace _Scripts.Fixed_Surfaces.Assembly
{
    public class AssemblySpot : Surface
    {
        private CircleCollider2D col;

        private new void Start()
        {
            base.Start();
            col = GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            col.enabled = transform.childCount == 0;
        }
    }
}