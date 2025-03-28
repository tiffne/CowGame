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
        public enum PatienceState
        {
            Patient,
            Neutral,
            Irritated,
            Done
        } 

        [SerializeField] private RecipesDatabase recipesDatabase;
        [SerializeField] private CustomersDatabase customersDatabase;

        public int patienceLevel = (int)PatienceState.Patient;
        
        private RecipeScriptableObject Order { get; set; }
        public string Species => chosenAnimal.name;
        public float TipAmount => Order.Price * (3 - patienceLevel);

        private Sprite customerSprite;
        private Sprite customersAccessory;
        private Sprite customersTop;
        private AnimalScriptableObject chosenAnimal;
        private SpriteRenderer bodySpriteRenderer;
        private SpriteRenderer accessorySpriteRenderer;
        private SpriteRenderer topSpriteRenderer;


        private new void Start()
        {
            base.Start();
            chosenAnimal = customersDatabase.Species[Random.Range(0, customersDatabase.Species.Count)];
            customersAccessory = customersDatabase.Accessories[Random.Range(0, customersDatabase.Accessories.Count)];
            customersTop = customersDatabase.Tops[Random.Range(0, customersDatabase.Tops.Count)];
            Order = recipesDatabase.Recipes[Random.Range(0, recipesDatabase.Recipes.Count)];

            name = Species;
            tag = "Customer";

            bodySpriteRenderer = GetComponent<SpriteRenderer>();
            var accessoryTransform = transform.GetChild(0);
            accessorySpriteRenderer = accessoryTransform.GetComponent<SpriteRenderer>();
            var topTransform = transform.GetChild(1);
            topSpriteRenderer = topTransform.GetComponent<SpriteRenderer>();

            bodySpriteRenderer.sprite = chosenAnimal.PatientSprite;

            // Get accessory name, set tag, and position it accordingly
            accessorySpriteRenderer.sprite = customersAccessory;
            var tempSpriteName = accessorySpriteRenderer.sprite.name;
            var tempTag = new string("");
            foreach (var letter in tempSpriteName)
            {
                if (letter == '_') break;
                tempTag += letter;
            }

            accessoryTransform.tag = tempTag;
            switch (accessoryTransform.tag)
            {
                case "Beanie":
                    accessoryTransform.transform.position += 3 * Vector3.up;
                    break;
                case "Earrings":
                    accessoryTransform.transform.position += 3 * Vector3.up + 1.5f * Vector3.forward;
                    break;
                case "Glasses":
                    accessoryTransform.transform.position += 2 * Vector3.up;
                    break;
            }

            // Get top name, set tag, and position it accordingly
            topSpriteRenderer.sprite = customersTop;
            tempSpriteName = topSpriteRenderer.sprite.name;
            tempTag = new string("");
            foreach (var letter in tempSpriteName)
            {
                if (letter == '_') break;
                tempTag += letter;
            }

            topTransform.tag = tempTag;
            switch (topTransform.tag)
            {
                case "DressShirt":
                    topTransform.transform.position += 1.2f * Vector3.down;
                    break;
                case "Hoodie":
                    topTransform.transform.position += 1.2f * Vector3.down;
                    break;
                case "Sweater":
                    topTransform.transform.position += 1.1f * Vector3.down;
                    break;
                case "Tank":
                    topTransform.transform.position += 1 * Vector3.down;
                    break;
                case "TShirt":
                    topTransform.transform.position += Vector3.down;
                    break;
            }
        }

        public void DoSomething()
        {
            Debug.Log("Clicked, wait...");
            StartCoroutine(LosePatience());
        }


        private IEnumerator LosePatience()
        {
            yield return new WaitForSeconds(2);
            switch (patienceLevel)
            {
                case (int)PatienceState.Patient:
                    bodySpriteRenderer.sprite = chosenAnimal.NeutralSprite;
                    bodySpriteRenderer.color = Color.yellow;
                    break;
                case (int)PatienceState.Neutral:
                    bodySpriteRenderer.sprite = chosenAnimal.IrritatedSprite;
                    bodySpriteRenderer.color = Color.red;
                    break;
                case (int)PatienceState.Irritated:
                    Debug.Log("See you in hell!");
                    break;
            }

            patienceLevel++;
            Debug.Log($"I am {Enum.GetName(typeof(PatienceState), patienceLevel)}");
        }
    }
}