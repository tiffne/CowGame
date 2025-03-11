using _Scripts.Player;
using UnityEngine;


namespace _Scripts.Ingredients
{
    public class Ingredient : Surface
    {
        private Hand _handLeft, _handRight;

        [SerializeField] protected IngredientScriptableObject ingredient;

        private string IngredientName => ingredient.IngredientName;

        [SerializeField] private Sprite[] ingredientSprites;

        protected bool GoesInBlender { get; set; }
        protected bool GoesInBurner { get; set; }

        protected bool IsBlended { get; set; } = false;
        protected bool IsCooked { get; set; } = false;
        protected bool IsBurned { get; set; } = false;
        protected bool IsMelted { get; set; } = false;

        private void Start()
        {
            GoesInBlender = ingredient.GoesInBlender;
            GoesInBurner = ingredient.GoesInBurner;
            ingredientSprites = ingredient.IngredientSprites;
            GetComponent<SpriteRenderer>().sprite = ingredientSprites[0];
            _handLeft = GameObject.Find("Hand Left").GetComponent<Hand>();
            _handRight = GameObject.Find("Hand Right").GetComponent<Hand>();
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(_handLeft.Index))
            {
                _handLeft.Interact(gameObject);
            }
            else if (Input.GetMouseButtonDown(_handRight.Index))
            {
                _handRight.Interact(gameObject);
            }
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