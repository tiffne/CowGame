using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Customer
{
    [CreateAssetMenu(fileName = "Customers Info", menuName = "Scriptable Objects/Customers Info")]
    public class CustomersDatabase : ScriptableObject
    {
        [SerializeField] private List<AnimalScriptableObject> species;
        public List<AnimalScriptableObject> Species => species;

        [SerializeField] private List<Sprite> accessories;
        public List<Sprite> Accessories => accessories;

[SerializeField] private List<Sprite> tops;
        public List<Sprite> Tops => tops;
    }
}