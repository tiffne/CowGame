using _Scripts.Fixed_Surfaces.Storing;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class Pocket : Container
    {
        private Inventory inventory;
        private BoxCollider col;

        private new void Start()
        {
            base.Start();
            col = GetComponent<BoxCollider>();
            AmountLeft = 0;
            tag = "Pocket";
            inventory = transform.parent.GetComponentInParent<Inventory>();
        }

        private void Update()
        {
            col.enabled = transform.childCount == 0;
        }

        private new void OnMouseOver()
        {
            inventory.OnMouseOver();
            base.OnMouseOver();
        }
    }
}