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
            Patient = 0,
            Neutral = 1,
            Irritated = 2,
            Done = 3
        }

        [SerializeField] private RecipesDatabase recipesDatabase;
        [SerializeField] private CustomersDatabase customersDatabase;
        [SerializeField] private GameObject speechOrderBubble;
        [SerializeField] private GameObject speechEvaluationBubble;

        public SpriteRenderer speechOrder;
        public SpriteRenderer speechEvaluation;

        public int PatienceLevel { get; set; } = (int)PatienceState.Patient;

        public RecipeScriptableObject Order { get; private set; }
        public bool IsServed { get; set; }
        public string Species => chosenAnimal.name;
        public float TipAmount => ButtonManager.lastButtonClicked.Equals(Species)
        ? Order.Price * (3 - PatienceLevel)
        : Order.Price * (4 - PatienceLevel);

        private bool canLosePatience = true;
        public bool beingRemoved = false;
        public bool hasTipped = false;
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

            speechOrder = speechOrderBubble.GetComponent<SpriteRenderer>();
            speechOrder.sprite = Order.RecipeSprite;

            speechEvaluation = speechEvaluationBubble.GetComponent<SpriteRenderer>();
            speechEvaluationBubble.SetActive(false);

            Debug.Log(Order.RecipeName);

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

        private void Update()
        {
            if (!canLosePatience) return;
            StartCoroutine(LosePatience());
        }

        private IEnumerator LosePatience()
        {
            canLosePatience = false;
            yield return new WaitForSeconds(20);
            switch (PatienceLevel)
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
                    // Debug.Log("See you in hell!");
                    break;
            }

            PatienceLevel++;
            canLosePatience = true;
            // Debug.Log($"I am {Enum.GetName(typeof(PatienceState), patienceLevel)}");
        }

        public IEnumerator ProvideFeedback()
        {
            if (beingRemoved) yield break;
            beingRemoved = true;
            speechOrderBubble.SetActive(false);
            speechEvaluationBubble.SetActive(true);
            var spriteColor = new Color();
            switch (PatienceLevel)
            {
                case (int)PatienceState.Patient:
                    spriteColor = Color.green;
                    break;
                case (int)PatienceState.Neutral:
                    spriteColor = Color.yellow;
                    break;
                case (int)PatienceState.Irritated:
                case (int)PatienceState.Done:
                    spriteColor = Color.red;
                    break;
            }

            if (PatienceLevel == (int)PatienceState.Done) speechEvaluationBubble.transform.Rotate(180, 0, 0);
            speechEvaluation.color = spriteColor;

            yield return new WaitForSeconds(2);
            CustomersManager.LineOfCustomers.RemoveCustomerFromLine(gameObject);
            transform.parent = null;
            SayByeBye();

            if (MoneyManager.Instance.TotalLostCustomer >= 5)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BadClosed");
            }
        }
    }
}
