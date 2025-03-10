using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Player
{
    public class Hand : MonoBehaviour
    {
        private enum Hands
        {
            Left = 0,
            Right = 1,
        }

        public int Index { get; private set; }
        private bool IsEmpty { get; set; } = true;
        
        private GameObject _itemInHand = null;

        private void Start()
        {
            Index = transform.name switch
            {
                "Hand Left" => (int)Hands.Left,
                "Hand Right" => (int)Hands.Right,
                _ => throw new ArgumentException("Invalid hand name")
            };
        }

        private void Update()
        {
            if (transform.childCount == 0) IsEmpty = true;
        }

        public void Interact(GameObject target)
        {
            if (target.CompareTag("Ingredient"))
            {
                GrabItem(target);
                return;
            }

            switch (target.tag)
            {
                case "AssemblyZone":
                    DropItem(target);
                    break;
            }
        }

        private void GrabItem(GameObject item)
        {
            if (!IsEmpty) return;
            _itemInHand = item;
            _itemInHand.transform.parent = transform;
            _itemInHand.transform.position = transform.position;
            IsEmpty = false;
        }

        private void DropItem(GameObject target)
        {
            if (IsEmpty) return;
            _itemInHand.transform.position = target.transform.position;
            _itemInHand.transform.parent = target.transform;
            _itemInHand = null;
        }

        // if (!IsEmpty)
        // {
        //     if (_itemInHand.TryGetComponent(out Ingredient ingredient))
        //     {
        //         if (surface.TryGetComponent(out AssemblySpot assemblySpot))
        //         {
        //             order = Instantiate(orderPrefab).GetComponent<Order>();
        //             order.transform.position = assemblySpot.transform.position + Vector3.back;
        //             order.CombineIngredient(ingredient);
        //         }
        //         else if (surface.TryGetComponent(out Order clickedOrder) && ingredient.IsReady)
        //         {
        //             clickedOrder.CombineIngredient(ingredient);
        //         }
        //         else if (surface.TryGetComponent(out Burner burner) && !ingredient.IsReady)
        //         {
        //             burner.Cook(ingredient);
        //         }
        //         else
        //         {
        //             return;
        //         }
        //     }
        //
        //     _itemInHand = null;
        //     _isEmpty = true;
    }
}