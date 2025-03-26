using System;
using System.Collections;
using _Scripts.Food.Recipes;
using UnityEngine;
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

        [SerializeField] private CustomersInfo infoPool;
        private Sprite customersBody;
        private Sprite customersHat;
        private Sprite customersShirt;
        public string Species { get; private set; }

        [SerializeField] private RecipesDatabase recipesDatabase;
        private RecipeScriptableObject Order { get; set; }

        private int patienceLevel = (int)PatienceState.Patient;

        private new void Start()
        {
            //Species = infoPool.Species[Random.Range(0, infoPool.Species.Count)];
            // customersBody = infoPool.CustomersBodies[Random.Range(0, infoPool.CustomersBodies.Count)];
            // customersHat = infoPool.CustomersHats[Random.Range(0, infoPool.CustomersHats.Count)];
            // customersShirt = infoPool.CustomersShirts[Random.Range(0, infoPool.CustomersShirts.Count)];
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