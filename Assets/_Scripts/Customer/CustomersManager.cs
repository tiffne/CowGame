using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static _Scripts.Customer.Customer.PatienceState;

namespace _Scripts.Customer
{
    public class CustomersManager : MonoBehaviour
    {
        private static class LineOfCustomers
        {
            private const int MaxLineSize = 3;
            public static List<GameObject> Line { get; } = new();
            public static bool IsFull => Line.Count >= MaxLineSize;

            public static void AddCustomerToLine(GameObject customer)
            {
                if (IsFull) return;
                Line.Add(customer);
            }

            public static void RemoveCustomerFromLine(GameObject customer)
            {
                Line.Remove(customer);
            }
        }

        [SerializeField] private GameObject customerPrefab;
        private bool CanAddNewCustomer { get; set; } = true;

        private void Update()
        {
            if (CanAddNewCustomer) StartCoroutine(AddCustomerCountdown());
            CheckOnCustomers();
        }

        private void CheckOnCustomers()
        {
            List<GameObject> customersToLeave = new();
            foreach (var customer in LineOfCustomers.Line)
            {
                if (customer.GetComponent<Customer>().patienceLevel == (int)Done)
                {
                    customersToLeave.Add(customer);
                }
            }

            if (customersToLeave.Count == 0) return;
            foreach (var customer in customersToLeave)
            {
                LineOfCustomers.RemoveCustomerFromLine(customer);
                customer.GetComponent<Customer>().SayByeBye();
            }

            CanAddNewCustomer = true;
        }

        private IEnumerator AddCustomerCountdown()
        {
            CanAddNewCustomer = false;
            yield return new WaitForSeconds(3);
            var tempCustomer = Instantiate(customerPrefab, transform, false);
            LineOfCustomers.AddCustomerToLine(tempCustomer);

            for (int i = 0; i < LineOfCustomers.Line.Count; i++)
            {
                var SpriteRenderer = LineOfCustomers.Line[i].GetComponent<SpriteRenderer>();
                if (SpriteRenderer != null)
                {
                    SpriteRenderer.sortingOrder = LineOfCustomers.Line.Count - 1 - i;
                }
            }

            CanAddNewCustomer = !LineOfCustomers.IsFull;
        }
    }
}