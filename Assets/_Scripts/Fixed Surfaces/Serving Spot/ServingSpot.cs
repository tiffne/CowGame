using _Scripts.Customer;
using _Scripts.Food;
using UnityEngine;
using static _Scripts.Customer.CustomersManager.LineOfCustomers;

namespace _Scripts.Fixed_Surfaces.Serving_Spot
{
    public class ServingSpot : Surface
    {
        private CustomersManager customersManager;
        private BoxCollider col;

        private new void Start()
        {
            base.Start();
            col = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            if (transform.childCount == 0)
            {
                col.enabled = true;
                return;
            }

            col.enabled = false;
            if (!transform.GetChild(0).TryGetComponent<Order>(out var order)) return;
            foreach (var customer in Line)
            {
                if (!customer.TryGetComponent<Customer.Customer>(out var cstmr) ||
                    !cstmr.Order.name.Equals(order.name)) continue;
                cstmr.IsServed = true;
                order.SayByeBye();
                break;
            }
        }
    }
}