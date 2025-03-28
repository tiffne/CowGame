using System;
using _Scripts.Fixed_Surfaces.Storing;
using _Scripts.Food;
using UnityEngine;

namespace _Scripts.Player
{
    public class Hand : MonoBehaviour
    {
        private enum Hands
        {
            Left = 0,
            Right = 1,
        }

        private bool IsEmpty { get; set; } = true;
        private bool CanInteract { get; set; } = true;
        public int Index { get; private set; }
        private GameObject _itemInHand;

        private void Start()
        {
            Index = transform.name switch
            {
                "Hand Left" => (int)Hands.Left,
                "Hand Right" => (int)Hands.Right,
                _ => throw new ArgumentException("Invalid hand name.")
            };
        }

        private void Update()
        {
            if (transform.childCount == 0) IsEmpty = true;
            if (!CanInteract) CanInteract = true;
        }

        /// <summary>
        /// Picks Up/Drops an Item from/on a Surface (e.g. Assembly Spots, Orders, Ingredients...). 
        /// <example>
        /// For example, if hand is empty and an Item is clicked, it will position that Item in the hand.
        /// If hand is not empty and a surface is clicked, it will position the Item on that Surface.
        /// The most common way to use it is:
        /// <code>
        /// handExample.Interact(gameObject);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="target"></param>
        public void Interact(GameObject target)
        {
            if (!CanInteract) return;
            CanInteract = false;

            switch (target.tag)
            {
                case "AssemblySpot":
                    DropItem(target);
                    break;
                case "Ingredient":
                case "Order":
                    var parent = target.transform.parent;
                    switch (IsEmpty)
                    {
                        case true:
                            if (parent.name.Equals("Shelf Spot")) parent.GetComponent<ShelfSpot>().ReduceAmountLeft();
                            GrabItem(target);
                            break;
                        case false:
                            if (parent.CompareTag("AssemblySpot"))
                            {
                                if (!(target.TryGetComponent<Order>(out var foo) && foo.IsReady)) DropItem(target);
                            }

                            break;
                    }

                    break;
                case "Pocket":
                    if (!_itemInHand.TryGetComponent<Order>(out var boo) || !boo.IsReady) DropItem(target);

                    break;
                case "Garbage":
                    DropItem(target);
                    break;

                case "Customer":
                    target.GetComponent<Customer.Customer>().DoSomething();
                    break;
            }
        }

        private void GrabItem(GameObject target)
        {
            if (!IsEmpty) return;
            _itemInHand = target;
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
    }
}