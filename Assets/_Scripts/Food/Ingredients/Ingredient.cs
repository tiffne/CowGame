using UnityEngine;

namespace _Scripts.Food.Ingredients
{
    public class Ingredient : Surface
    {
        [SerializeField] private Order orderPrefab;
        [SerializeField] protected IngredientScriptableObject ingredient;
        [SerializeField] private Sprite[] ingredientSprites;

        public string IngredientName => name;

        public bool CanBlend { get; set; }
        public bool CanCook { get; set; }
        public bool CanMelt { get; set; }

        public float TimeToBlend { get; set; }
        public float TimeToCook { get; set; }
        public float TimeToMelt { get; set; }

        protected bool IsBlended { get; set; } = false;
        protected bool IsCooked { get; set; } = false;
        protected bool IsBurned { get; set; } = false;
        protected bool IsMelted { get; set; } = false;

        private new void Start()
        {
            base.Start();
            tag = "Ingredient";
            CanBlend = ingredient.CanBlend;
            CanCook = ingredient.CanCook;
            CanMelt = ingredient.CanMelt;

            TimeToBlend = ingredient.TimeToBlend;
            TimeToCook = ingredient.TimeToCook;
            TimeToMelt = ingredient.TimeToMelt;

            ingredientSprites = ingredient.IngredientSprites;
            GetComponent<SpriteRenderer>().sprite = ingredientSprites[0];
        }

        private void Update()
        {
            if (transform.childCount == 0) return;
            MergeIntoOrder(transform.GetChild(0).gameObject);
        }

        private void MergeIntoOrder(GameObject target)
        {
            var order = Instantiate(orderPrefab).gameObject.GetComponent<Order>();
            order.MergeIngredients(new[] {gameObject, target});
        }

        public void SayByeBye() // transform this in a method that returns the ingredients to the pantry
        {
            Destroy(gameObject);
        }
    }
}

// using UnityEngine;
//
//
//     public class Ingredient : Surface
//     {
//         private bool _isReady = true;
//         public bool IsReady
//         {
//             get { return _isReady; }
//             set
//             {
//                 _isReady = value;
//                 if (IsReady && transform.CompareTag("Patty") && TryGetComponent(out SpriteRenderer sprite))
//                 {
//                     sprite.sprite = cookedPatty;
//                 }
//             }
//         }
//
//         float _cookingTime;
//         public float CookingTime
//         {
//             get { return _cookingTime; }
//         }
//
//         public Sprite cookedPatty;
//         private SpriteRenderer spriteRenderer; 
//
//         new void Start()
//         {
//             base.Start();
//             if (transform.CompareTag("Patty"))
//             {
//                 IsReady = false;
//                 _cookingTime = 5.0f;
//                 spriteRenderer = GetComponent<SpriteRenderer>();
//             }
//             else
//             {
//                 IsReady = true;
//             }
//         }
//
//         public void OnMouseDown()
//         {
//             if (HandLeft.IsEmpty)
//             {
//                 HandLeft.GrabItem(this);
//             }
//             else
//             {
//                 HandLeft.DropItem(this);
//             }
//         }
//
//         public void ReturnToPocket()
//         {
//             switch (gameObject.tag)
//             {
//                 case "Bun":
//                     transform.position = new Vector3(1, -3, -1);
//                     break;
//                 case "Cheese":
//                     transform.position = new Vector3(-1, -3, -1);
//                     break;
//                 case "Patty":
//                     transform.position = new Vector3(-3, -3, -1);
//                     break;
//             }
//         }
//     }