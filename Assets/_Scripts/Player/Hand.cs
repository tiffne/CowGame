using System;
using _Scripts.Fixed_Surfaces.Storing;
using _Scripts.Food;
using _Scripts.Food.Ingredients._Ingredient;
using UnityEditorInternal;
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


        private bool IsEmpty { get; set; }
        // [SerializeField] private bool _isEmpty = true;
        //
        // private bool IsEmpty
        // {
        //     get => _isEmpty;
        //     private set => _isEmpty = value;
        // }

        private bool CanInteract { get; set; } = true;
        public int Index { get; private set; }
        private GameObject _itemInHand;

        [SerializeField] private AudioSource pickUpSound;

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
                case "Serving Spot":
                    if (IsEmpty) GrabItem(target);
                    else DropItem(target);
                    break;
                case "Shelf Spot":
                    if (IsEmpty) GrabItem(target.GetComponent<ShelfSpot>().GetRespectiveItem());
                    break;
                case "AssemblySpot":
                    DropItem(target);
                    break;
                case "Ingredient":
                case "Order":
                    if (IsEmpty) GrabItem(target);
                    else if (target.TryGetComponent<Ingredient>(out var ing3) &&
                             _itemInHand.TryGetComponent<Order>(out var ord3))
                    {
                        ing3.GenerateNewOrder(null);
                    }
                    else if (target.transform.parent.CompareTag("AssemblySpot"))
                    {
                        // If item in hand is a Ready Ingredient OR Not Ready Order AND the target is not a complete order
                        if (((_itemInHand.TryGetComponent<Order>(out var order1) && !order1.IsReady) ||
                             (_itemInHand.TryGetComponent<Ingredient>(out var ing1) && ing1.IsReady)) &&
                            !(target.TryGetComponent<Order>(out var order2) && order2.IsReady))
                        {
                            if ((_itemInHand.name.Equals("Plate") || _itemInHand.name.Equals("Cup") ||
                                 (order1 != null && order1.HasTableware)) &&
                                (order2 != null && order2.HasTableware || target.name.Equals("Plate") ||
                                 target.name.Equals("Cup")))
                            {
                                return;
                            }

                            DropItem(target);
                        }
                    }

                    break;
                case "Pocket":
                    if (!IsEmpty && (!_itemInHand.TryGetComponent<Order>(out var order3) || !order3.HasTableware)
                                 && !(_itemInHand.name.Equals("Plate") || _itemInHand.name.Equals("Cup")))
                        DropItem(target);

                    break;
                case "Garbage":
                    DropItem(target);
                    break;

                case "Customer":
                    target.GetComponent<Customer.Customer>().DoSomething();
                    break;

                case "Burner":
                    if (!IsEmpty && _itemInHand.TryGetComponent<Ingredient>(out var foo) && foo.CanCook)
                    {
                        DropItem(target);
                    }

                    break;
                case "Blender":
                    if (!IsEmpty && _itemInHand.TryGetComponent<Ingredient>(out var boo) && boo.CanBlend)
                    {
                        DropItem(target);
                    }

                    break;
      
            }
        }

        private void GrabItem(GameObject target)
        {
            if (!IsEmpty) return;
            _itemInHand = target;
            _itemInHand.transform.parent = transform;
            //Following 2 lines have been adjusted so that 1) Items sit closer to center of paw, and 2) Items increase in scale to emulate perspective
            _itemInHand.transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
            _itemInHand.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            IsEmpty = false;
            pickUpSound.Play();
        }

        private void DropItem(GameObject target)
        {
            if (IsEmpty) return;
            _itemInHand.transform.position = target.transform.position;
            _itemInHand.transform.parent = target.transform;
            //Following line has been added so that items decrease in scale to emulate perspective
            _itemInHand.transform.localScale = new Vector3(1, 1, 1);
            _itemInHand = null;
        }
    }
}