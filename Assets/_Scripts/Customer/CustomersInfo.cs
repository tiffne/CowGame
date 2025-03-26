using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Customer
{
    [CreateAssetMenu(fileName = "Customers Info", menuName = "Scriptable Objects/Customers Info")]
    public class CustomersInfo : ScriptableObject
    {
        [SerializeField] private List<string> species;
        public List<string> Species => species;

        [SerializeField] private List<Sprite> customersBodies;
        public List<Sprite> CustomersBodies => customersBodies;

        [SerializeField] private List<Sprite> customersHats;
        public List<Sprite> CustomersHats => customersHats;

        [SerializeField] private List<Sprite> customersShirts;
        public List<Sprite> CustomersShirts => customersShirts;
    }
}