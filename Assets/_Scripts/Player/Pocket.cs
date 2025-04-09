using _Scripts.Fixed_Surfaces.Storing;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class Pocket : Container
    {
        public static Inventory Inventory { get; private set; }
        private BoxCollider col;

        private new void Start()
        {
            base.Start();
            col = GetComponent<BoxCollider>();
            AmountLeft = 0;
            tag = "Pocket";
            Inventory = transform.parent.GetComponentInParent<Inventory>();
        }

        private void Update()
        {
            col.enabled = transform.childCount == 0;
        }

        private new void OnMouseOver()
        {
            Inventory.OnMouseOver();
            base.OnMouseOver();
        }
    }
}