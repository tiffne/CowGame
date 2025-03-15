using System;
using UnityEngine;

namespace _Scripts.Food.Ingredients._Ingredient
{
    public class Ingredient : Surface, IComparable
    {
        [SerializeField] private Order orderPrefab;
        [SerializeField] protected IngredientScriptableObject ingredient;

        private Sprite _ingredientRawSprite;
        private Sprite _ingredientBlendedSprite;
        private Sprite _ingredientCookedSprite;
        private Sprite _ingredientBurnedSprite;
        private Sprite _ingredientMeltedSprite;

        public string IngredientName => name;

        public int CompareTo(object obj)
        {
            if (obj == null) throw new NullReferenceException("Can't compare null ingredients.");
            var otherIngredient = obj as Ingredient;
            if (otherIngredient)
            {
                return string.CompareOrdinal(IngredientName, otherIngredient.IngredientName);
            }

            throw new ArgumentException("Object not an Ingredient.");
        }

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

            _ingredientRawSprite = ingredient.IngredientRawSprite;
            _ingredientBlendedSprite = ingredient.IngredientBlendedSprite;
            _ingredientCookedSprite = ingredient.IngredientCookedSprite;
            _ingredientBurnedSprite = ingredient.IngredientBurnedSprite;
            _ingredientMeltedSprite = ingredient.IngredientMeltedSprite;

            GetComponent<SpriteRenderer>().sprite = _ingredientRawSprite;
        }

        private void Update()
        {
            if (transform.childCount == 0) return;
            GenerateNewOrder(transform.GetChild(0).gameObject);
        }

        private void GenerateNewOrder(GameObject target)
        {
            var order = Instantiate(orderPrefab, transform.parent).gameObject.GetComponent<Order>();
            order.MergeIngredients(new[] { gameObject, target });
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