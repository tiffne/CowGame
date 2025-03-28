using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Customer
{
    public class CustomersManager : MonoBehaviour
    {
        private static class LineOfCustomers
        {
            private const int MaxLineSize = 5;
            public static List<GameObject> Line { get; } = new();
            private static bool IsFull => Line.Count >= MaxLineSize;

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
        private bool CanAddNewCustomer { get; set; } = false;

        private void Update()
        {
            if (CanAddNewCustomer) StartCoroutine(AddCustomerCountdown());
        }

        private void CheckOnCustomers()
        {
            List <GameObject> customersToLeave = new();
            foreach (var customer in LineOfCustomers.Line)
            {
                if (customer.GetComponent<Customer>().patienceLevel == 4)
                {
                    customersToLeave.Add(customer);
                    
                }
            }
        }

        private IEnumerator AddCustomerCountdown()
        {
            yield return new WaitForSeconds(5);
            LineOfCustomers.AddCustomerToLine(Instantiate(customerPrefab));
        }
    }
}