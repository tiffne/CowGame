using UnityEngine;

namespace _Scripts.Fixed_Surfaces.Assembly
{
    public class AssemblySpot : Surface
    {
        private BoxCollider2D col;

        private new void Start()
        {
            base.Start();
            col = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            col.enabled = transform.childCount == 0;
        }
    }
}