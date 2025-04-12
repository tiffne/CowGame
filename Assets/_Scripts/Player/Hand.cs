using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Fixed_Surfaces.Storing;
using _Scripts.Food;
using _Scripts.Food.Ingredients._Ingredient;
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
        private GameObject itemInHand;
        private bool thoughtBubbleActive = false;

        [SerializeField] private AudioSource pickUpSound;
        [SerializeField] private AudioSource thoughtBubbleSound;
        [SerializeField] private GameObject thoughtBubble;

        private void Start()
        {
            Index = transform.name switch
            {
                "Hand Left" => (int)Hands.Left,
                "Hand Right" => (int)Hands.Right,
                _ => throw new ArgumentException("Invalid hand name.")
            };
            thoughtBubble.SetActive(false);
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
                    DropItem(target);
                    break;
                case "Shelf Spot":
                    if (IsEmpty) GrabItem(target.GetComponent<ShelfSpot>().GetRespectiveItem());
                    else
                    {
                        StartCoroutine(EnableThoughtBubble());
                    }

                    break;
                case "AssemblySpot":
                    DropItem(target);
                    break;
                case "Ingredient":
                case "Order":
                    if (IsEmpty) GrabItem(target);
                    else if ((itemInHand.name.Equals("Plate") || itemInHand.name.Equals("Cup")) &&
                             (target.name.Equals("Plate") || target.name.Equals("Cup")))
                    {
                        StartCoroutine(EnableThoughtBubble());
                    }
                    else if (target.TryGetComponent<Ingredient>(out var ing1) &&
                             itemInHand.TryGetComponent<Order>(out _))
                    {
                        ing1.GenerateNewOrder(null);
                    }
                    else if (target.transform.parent.CompareTag("AssemblySpot"))
                    {
                        var order1 = itemInHand.GetComponent<Order>();
                        var ing2 = itemInHand.GetComponent<Ingredient>();
                        if ((order1 && !order1.IsReady) || (ing2 && ing2.IsReady))
                        {
                            if (target.TryGetComponent<Order>(out var order2) && order2.IsReady)
                            {
                                StartCoroutine(EnableThoughtBubble());
                                return;
                            }

                            if (target.TryGetComponent<Ingredient>(out var ing3) && !ing3.IsReady)
                            {
                                StartCoroutine(EnableThoughtBubble());
                                return;
                            }

                            DropItem(target);
                        }
                        else
                        {
                            StartCoroutine(EnableThoughtBubble());
                        }
                    }
                    else
                    {
                        StartCoroutine(EnableThoughtBubble());
                    }

                    break;
                case "Pocket":
                    if (!IsEmpty && (!itemInHand.TryGetComponent<Order>(out var order3) || !order3.HasTableware)
                                 && !(itemInHand.name.Equals("Plate") || itemInHand.name.Equals("Cup")))
                    {
                        DropItem(target);
                    }
                    else
                    {
                        StartCoroutine(EnableThoughtBubble());
                    }

                    break;
                case "Garbage":
                    DropItem(target);
                    break;

                case "Customer":
                    target.GetComponent<Customer.Customer>().DoSomething();
                    break;

                case "Burner":
                    if (!IsEmpty && itemInHand.TryGetComponent<Ingredient>(out var foo) && foo.CanCook)
                    {
                        DropItem(target);
                    }
                    else
                    {
                        StartCoroutine(EnableThoughtBubble());
                    }

                    break;
                case "Blender":
                    if (!IsEmpty && itemInHand.TryGetComponent<Ingredient>(out var boo) && boo.CanBlend)
                    {
                        DropItem(target);
                    }
                    else
                    {
                        StartCoroutine(EnableThoughtBubble());
                    }

                    break;
                default:
                    StartCoroutine(EnableThoughtBubble());
                    break;
            }
        }

        private void GrabItem(GameObject target)
        {
            if (!IsEmpty) return;
            itemInHand = target;
            itemInHand.transform.parent = transform;
            //Following 2 lines have been adjusted so that 1) Items sit closer to center of paw, and 2) Items increase in scale to emulate perspective
            itemInHand.transform.position =
                new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
            itemInHand.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            IsEmpty = false;
            pickUpSound.Play();
        }

        private void DropItem(GameObject target)
        {
            if (IsEmpty) return;
            itemInHand.transform.position = target.transform.position;
            itemInHand.transform.parent = target.transform;
            //Following line has been added so that items decrease in scale to emulate perspective
            itemInHand.transform.localScale = new Vector3(1, 1, 1);
            itemInHand = null;
        }

        private IEnumerator EnableThoughtBubble()
        {
            if (thoughtBubbleActive) yield break;
            thoughtBubble.SetActive(true);
            thoughtBubbleActive = true;
            thoughtBubbleSound.Play();
            yield return new WaitForSeconds(2);
            thoughtBubble.SetActive(false);
            thoughtBubbleActive = false;
        }
    }
}