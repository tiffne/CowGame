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

        [FormerlySerializedAs("infoPool")] [SerializeField]
        private CustomersDatabase customersDatabase;

        private Sprite customerSprite;
        private Sprite customersAccessory;
        private Sprite customersTop;
        private AnimalScriptableObject chosenAnimal;
        public string Species => chosenAnimal.name;
        private SpriteRenderer spriteRenderer;

        [SerializeField] private RecipesDatabase recipesDatabase;
        private RecipeScriptableObject Order { get; set; }

        private int patienceLevel = (int)PatienceState.Patient;

        private new void Start()
        {
            base.Start();
            chosenAnimal = customersDatabase.Species[Random.Range(0, customersDatabase.Species.Count)];
            customersAccessory = customersDatabase.Accessories[Random.Range(0, customersDatabase.Accessories.Count)];
            customersTop = customersDatabase.Tops[Random.Range(0, customersDatabase.Tops.Count)];
            Order = recipesDatabase.Recipes[Random.Range(0, recipesDatabase.Recipes.Count)];
            Debug.Log($"My Species is {Species}");

            name = Species;
            tag = "Customer";

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = chosenAnimal.PatientSprite;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void DoSomething()
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
            yield return new WaitForSeconds(2);
            switch (patienceLevel)
            {
                case (int)PatienceState.Patient:
                    spriteRenderer.sprite = chosenAnimal.NeutralSprite;
                    spriteRenderer.color = Color.yellow;
                    break;
                case (int)PatienceState.Neutral:
                    spriteRenderer.sprite = chosenAnimal.IrritatedSprite;
                    spriteRenderer.color = Color.red;
                    break;
                case (int)PatienceState.Irritated:
                    Debug.Log("See you in hell!");
                    Destroy(gameObject);
                    break;
            }

            patienceLevel++;
            Debug.Log($"I am {Enum.GetName(typeof(PatienceState), patienceLevel)}");
        }
    }
}