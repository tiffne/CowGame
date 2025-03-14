using UnityEngine;
using System;
using System.Collections.Generic;
using _Scripts.Food.Ingredients;
using _Scripts.Food.Recipes;
using Unity.VisualScripting;
using Update = UnityEngine.PlayerLoop.Update;

namespace _Scripts
{
    public class Order : Surface
    {
        [SerializeField] private RecipesDatabase recipes;
        private readonly List<Ingredient> _ingredients = new();
        private RecipeScriptableObject _matchedRecipe;
        private readonly Dictionary<string, Sprite> _spritesDict = new();
        private bool isReady = false;

        private new void Start()
        {
            base.Start();
            foreach (var recipe in recipes.Recipes)
            {
                _spritesDict[recipe.RecipeName] = recipe.RecipeSprite;
            }
        }

        private void Update()
        {
            if (isReady) return;
            if (!IsOrderComplete()) return;
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Ingredient>().SayByeBye();
            }

            Debug.Log(_matchedRecipe.name);
            transform.GetComponent<SpriteRenderer>().sprite = _spritesDict[_matchedRecipe.name];
            isReady = true;
        }

        public void MergeIngredients(GameObject[] ingredients)
        {
            if (ingredients.Length == 0) return;

            transform.parent = ingredients[0].transform.parent;
            transform.position = ingredients[0].transform.position;

            foreach (var ingredient in ingredients)
            {
                ingredient.transform.parent = transform;
                ingredient.GetComponent<Collider>().enabled = false;
                _ingredients.Add(ingredient.GetComponent<Ingredient>());
            }
        }

        private bool IsOrderComplete()
        {
            foreach (var recipe in recipes.Recipes)
            {
                if (recipe.Ingredients.Count != _ingredients.Count) continue;
                for (var i = 0; i < recipe.Ingredients.Count; i++)
                {
                    if (!recipe.Ingredients[i].IngredientName.Equals(_ingredients[i].IngredientName))
                        return false; //this code is wrong, it's just for testing
                }

                // if (CheckMatch(recipe.Ingredients, _ingredients)) return true;

                _matchedRecipe = recipe;
            }

            return true;
        }

        // Not my code, but it works. I'll analyze it later to see if there's a better way
        // private static bool CheckMatch<T>(List<T> aListA, List<T> aListB)
        // {
        //     if (aListA == null || aListB == null || aListA.Count != aListB.Count)
        //         return false;
        //     if (aListA.Count == 0)
        //         return true;
        //
        //     Dictionary<T, int> lookUp = new Dictionary<T, int>();
        //     // create index for the first list
        //     for (int i = 0; i < aListA.Count; i++)
        //     {
        //         int count = 0;
        //         if (!lookUp.TryGetValue(aListA[i], out count))
        //         {
        //             lookUp.Add(aListA[i], 1);
        //             continue;
        //         }
        //
        //         lookUp[aListA[i]] = count + 1;
        //     }
        //
        //     for (int i = 0; i < aListB.Count; i++)
        //     {
        //         int count = 0;
        //         if (!lookUp.TryGetValue(aListB[i], out count))
        //         {
        //             // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
        //             return false;
        //         }
        //
        //         count--;
        //         if (count <= 0)
        //             lookUp.Remove(aListB[i]);
        //         else
        //             lookUp[aListB[i]] = count;
        //     }
        //
        //     // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
        //     return lookUp.Count == 0;
        // }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using _Scripts.Ingredients;
// using _Scripts.Player.Hands;
// using UnityEngine;
//
// public class Order : Surface
// {
//     private readonly List<string> _recipe = new() { "Bun", "Cheese", "Patty" };
//     public List<string> ingredientsInOrder = new();
//
//
//     public void CombineIngredient(Ingredient ingredient)
//     {
//         if (!ingredient.IsReady)
//         {
//             return;
//         }
//
//         ingredientsInOrder.Add(ingredient.tag);
//         ingredientsInOrder.Sort();
//         ingredient.GetComponent<Collider2D>().enabled = false;
//         ingredient.transform.parent = transform;
//         ingredient.transform.position =
//             transform.position + Vector3.forward + Vector3.up * (ingredientsInOrder.Count - 1);
//         if (IsOrderComplete())
//         {
//             StartCoroutine(CongratulateAndDestroy());
//         }
//     }
//
//     bool IsOrderComplete()
//     {
//         if (ingredientsInOrder.Count != _recipe.Count)
//         {
//             return false;
//         }
//
//         for (var i = 0; i < _recipe.Count; i++)
//         {
//             if (ingredientsInOrder[i] != _recipe[i])
//             {
//                 return false;
//             }
//         }
//
//         return true;
//     }
//
//     IEnumerator CongratulateAndDestroy()
//     {
//         Debug.Log("Order Done!");
//         yield return new WaitForSeconds(1);
//         Destroy(this.gameObject);
//     }
// }