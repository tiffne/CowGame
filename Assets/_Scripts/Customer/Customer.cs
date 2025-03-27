using System;
using System.Collections;
using _Scripts.Food.Recipes;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Scripts.Customer
{
    public class Customer : Surface
    {
        private enum PatienceState
        {
            Patient,
            Neutral,
            Irritated,
            Done
        }

        [FormerlySerializedAs("infoPool")] [SerializeField] private CustomersDatabase customersDatabase;
        private Sprite customerSprite;
        private Sprite customersAccessory;
        private Sprite customersTop;
        private AnimalScriptableObject chosenAnimal;
        public string Species => chosenAnimal.name;
        private Sprite currentSprite; 

        [SerializeField] private RecipesDatabase recipesDatabase;
        private RecipeScriptableObject Order { get; set; }

        private int patienceLevel = (int)PatienceState.Patient;

        private new void Start()
        {
            chosenAnimal = customersDatabase.Species[Random.Range(0, customersDatabase.Species.Count)];
            currentSprite = chosenAnimal.PatientSprite;
            customersAccessory = customersDatabase.Accessories[Random.Range(0, customersDatabase.Accessories.Count)];
            customersTop = customersDatabase.Tops[Random.Range(0, customersDatabase.Tops.Count)];
            Order = recipesDatabase.Recipes[Random.Range(0, recipesDatabase.Recipes.Count)];
            Debug.Log($"I am {Enum.GetName(typeof(PatienceState), patienceLevel)}");
            tag = "Customer";
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnMouseDown()
        {
            Debug.Log("Clicked, wait...");
            StartCoroutine(LosePatience());
        }

        public float TipAmount()
        {
            return Order.Price * (3 - patienceLevel);
        }

        private IEnumerator LosePatience()
        {
            yield return new WaitForSeconds(5);
            patienceLevel++;
            Debug.Log($"I am {Enum.GetName(typeof(PatienceState), patienceLevel)}");
        }
    }
}