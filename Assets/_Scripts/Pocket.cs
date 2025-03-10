// using _Scripts.Ingredients;
// using UnityEngine;
//
// namespace _Scripts.Player
// {
//     public class Pocket : MonoBehaviour
//     {
//         private Ingredient _ingredientHeld;
//         [SerializeField] private GameObject pattyPrefab;
//         [SerializeField] private GameObject cheesePrefab;
//         [SerializeField] private GameObject bunPrefab;
//
//
//         private void Update()
//         {
//             if (transform.childCount == 0)
//             {
//                 InstantiateIngredients();
//             }
//         }
//
//         void InstantiateIngredients()
//         {
//             switch (tag)
//             {
//                 case "Patty":
//                     _ingredientHeld = Instantiate(pattyPrefab).GetComponent<Ingredient>();
//                     break;
//                 case "Cheese":
//                     _ingredientHeld = Instantiate(cheesePrefab).GetComponent<Ingredient>();
//                     break;
//                 case "Bun":
//                     _ingredientHeld = Instantiate(bunPrefab).GetComponent<Ingredient>();
//                     break;
//             }
//             _ingredientHeld.transform.parent = transform;
//             _ingredientHeld.transform.position = transform.position;
//         }
//     }
// }
