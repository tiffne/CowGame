using _Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;


namespace _Scripts.Ingredients
{
    public class Ingredient : Surface
    {
        [SerializeField] private Order orderPrefab;
        [SerializeField] protected IngredientScriptableObject ingredient;
        [SerializeField] private Sprite[] ingredientSprites;

        private string IngredientName => name;
        protected bool CanBlend { get; set; }
        protected bool CanCook { get; set; }
        protected bool CanMelt { get; set; }

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
            ingredientSprites = ingredient.IngredientSprites;
            GetComponent<SpriteRenderer>().sprite = ingredientSprites[0];
        }

        private void Update()
        {
            if (transform.childCount == 0) return;
            MergeIntoOrder(transform.GetChild(0).gameObject);
        }

        public void MergeIntoOrder(GameObject target)
        {
            var order = Instantiate(orderPrefab).gameObject;

            order.transform.parent = transform.parent;
            order.transform.position = transform.position;
            
            target.transform.parent = order.transform;
            gameObject.transform.parent = order.transform;
            
            target.GetComponent<Collider>().enabled = false;
            GetComponent<Collider>().enabled = false;

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