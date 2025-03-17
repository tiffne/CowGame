using _Scripts.Fixed_Surfaces.Storing;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Pocket : Container
    {
        private BoxCollider2D col;

        private new void Start()
        {
            base.Start();
            col = GetComponent<BoxCollider2D>();
            AmountLeft = 0;
            tag = "Pocket";
        }

        private void Update()
        {
            col.enabled = transform.childCount == 0;
        }
    }
}