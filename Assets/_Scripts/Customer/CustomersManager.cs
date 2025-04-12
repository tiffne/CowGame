using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static _Scripts.Customer.Customer.PatienceState;

namespace _Scripts.Customer
{
    public class CustomersManager : MonoBehaviour
    {
        [SerializeField] private AudioSource customerEnterSound;
        [SerializeField] private AudioSource customerDoneSound;

        public static class LineOfCustomers
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
                Debug.Log(customer.GetComponent<Customer>().patienceLevel);
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
                var cstmr = customer.GetComponent<Customer>();
                if (cstmr.IsServed)
                {
                    MoneyManager.Instance.AddTip(cstmr);
                    customersToLeave.Add(customer);
                }

                else if (cstmr.patienceLevel == (int)Done)
                {
                    customersToLeave.Add(customer);
                }
            }

            if (customersToLeave.Count == 0) return;
            foreach (var customer in customersToLeave)
            {
                customerDoneSound.Play();
                LineOfCustomers.RemoveCustomerFromLine(customer);
                customer.transform.parent = null;
                customer.GetComponent<Customer>().SayByeBye();
            }

            CanAddNewCustomer = true;
        }

        private IEnumerator AddCustomerCountdown()
        {
            CanAddNewCustomer = false;
            yield return new WaitForSeconds(3);
            customerEnterSound.Play();
            var tempCustomer = Instantiate(customerPrefab, transform, false);
            LineOfCustomers.AddCustomerToLine(tempCustomer);


            for (var i = 0; i < LineOfCustomers.Line.Count; i++)
            {
                var zPos = 7 - 7 * i;
                LineOfCustomers.Line[i].transform.position = new Vector3(transform.position.x, 1.4f, -10 + zPos);
            }

            CanAddNewCustomer = !LineOfCustomers.IsFull;
        }
    }
}