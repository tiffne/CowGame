using _Scripts.Player;
using UnityEngine;


namespace _Scripts.Ingredients
{
    public class Ingredient : Surface
    {
        [SerializeField] protected IngredientScriptableObject ingredient;

        private string IngredientName => name;

        [SerializeField] private Sprite[] ingredientSprites;

        protected bool GoesInBlender { get; set; }
        protected bool GoesInBurner { get; set; }

        protected bool IsBlended { get; set; } = false;
        protected bool IsCooked { get; set; } = false;
        protected bool IsBurned { get; set; } = false;
        protected bool IsMelted { get; set; } = false;

        private new void Start()
        {
            base.Start();
            tag = "Ingredient";
            GoesInBlender = ingredient.GoesInBlender;
            GoesInBurner = ingredient.GoesInBurner;
            ingredientSprites = ingredient.IngredientSprites;
            GetComponent<SpriteRenderer>().sprite = ingredientSprites[0];
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