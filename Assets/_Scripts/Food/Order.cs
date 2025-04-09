using System;
using System.Collections.Generic;
using _Scripts.Food.Ingredients._Ingredient;
using _Scripts.Food.Recipes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Food
{
    public class Order : Surface
    {
        [SerializeField] private RecipesDatabase recipes;
        private readonly List<Ingredient> _ingredients = new();
        private RecipeScriptableObject _matchedRecipe;
        private readonly Dictionary<string, Sprite> _spritesDict = new();
        public bool IsReady { get; private set; }

        public bool IsPerfect { get; private set; }
        public bool HasTableware { get; private set; }

        private new void Start()
        {
            base.Start();
            IsPerfect = true;
            foreach (var recipe in recipes.Recipes)
            {
                recipe.Ingredients.Sort();
                _spritesDict[recipe.RecipeName] = recipe.RecipeSprite;
                _spritesDict[recipe.RecipeName + "_rawA"] = recipe.RawASprite;
                _spritesDict[recipe.RecipeName + "_burntA"] = recipe.BurntASprite;
                _spritesDict[recipe.RecipeName + "_rawB"] = recipe.RawBSprite;
                _spritesDict[recipe.RecipeName + "_burntB"] = recipe.BurntBSprite;
                _spritesDict[recipe.RecipeName + "_rawAB"] = recipe.RawABSprite;
                _spritesDict[recipe.RecipeName + "_burntAB"] = recipe.BurntABSprite;
                _spritesDict[recipe.RecipeName + "_rawAburntB"] = recipe.RawABurntBSprite;
                _spritesDict[recipe.RecipeName + "_burntArawB"] = recipe.BurntARawBSprite;
            }
        }

        private void Update()
        {
            if (IsReady) return;
            if (!IsOrderComplete()) return;

            int totalCookAmt = 0;
            int totalCookable = 0;

            int steakCookAmt = 0;

            string cookState = "";

            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).GetComponent<Ingredient>();
                if (child.CanCook)
                {
                    totalCookable += 1;
                    totalCookAmt += child.CookAmt;
                }

                if (child.gameObject.name.Equals("Steak"))
                {
                    steakCookAmt = child.CookAmt;
                }
            }

            if (!_matchedRecipe.name.Equals("Meatshake"))
            {

                if (totalCookable == 1)
                {
                    switch (totalCookAmt)
                    {
                        case 0:
                            cookState = "_rawA";
                            break;
                        case 2:
                            cookState = "_burntA";
                            break;

                    }
                }

                else if (totalCookable > 1)
                {
                    switch (totalCookAmt)
                    {
                        case 0:
                            cookState = "_rawAB";
                            break;

                        case 1:
                            if (steakCookAmt == 1)
                            {
                                cookState = "_rawB";
                            }
                            else cookState = "_rawA";
                            break;

                        case 2:
                            if (steakCookAmt == 1)
                            {
                                cookState = "_rawAB";
                            }
                            else if (steakCookAmt == 2)
                            {
                                cookState = "_burntArawB";
                            }
                            else cookState = "_rawAburntB";
                            break;

                        case 3:
                            if (steakCookAmt == 1)
                            {
                                cookState = "_burntB";
                            }
                            else cookState = "_burntA";
                            break;

                        case 4:
                            cookState = "_burntAB";
                            break;


                    }
                }
            }


            for (var i = 0; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i).GetComponent<Ingredient>();
                    if (child.gameObject.name.Equals("Plate")) continue;
                    child.SayByeBye();
                }


                if (cookState.Contains("A") || cookState.Contains("B")) IsPerfect = false;
                name = _matchedRecipe.name;
                transform.GetComponent<SpriteRenderer>().sprite = _spritesDict[name + cookState];
                IsReady = true;
            
        }

        public void MergeIngredients(GameObject[] ingredients)
        {
            if (ingredients.Length == 0) return;

            transform.position = ingredients[0].transform.position;

            foreach (var ingredient in ingredients)
            {
                if (ingredient.name.Equals("Plate") || ingredient.name.Equals("Cup")) HasTableware = true;
                ingredient.transform.parent = transform;
                ingredient.GetComponent<Collider>().enabled = false;
                _ingredients.Add(ingredient.GetComponent<Ingredient>());
            }
        }

        private bool IsOrderComplete()
        {
            if (_ingredients.Count < transform.childCount)
            {
                _ingredients.Add(transform.GetChild(transform.childCount - 1).GetComponent<Ingredient>());
            }

            foreach (var recipe in recipes.Recipes)
            {
                if (!CheckIsMatch(recipe)) continue;
                _matchedRecipe = recipe;
                return true;
            }

            return false;
        }

        private bool CheckIsMatch(RecipeScriptableObject recipe)
        {
            if (recipe.Ingredients.Count != _ingredients.Count) return false;

            // TODO: To Betmann, find a better way to sort. This is a personal challenge, not essential for submission.
            _ingredients.Sort();
            for (var i = 0; i < recipe.Ingredients.Count; i++)
            {
                var ingName = _ingredients[i].name;
                var ingRecName = recipe.Ingredients[i].name;
                if (_ingredients[i].name != recipe.Ingredients[i].name) return false;
            }

            return true;
        }

        private new void OnMouseOver()
        {
            base.OnMouseOver();

            Transform orderChild;
            try
            {
                orderChild = transform.GetChild(transform.childCount - 1);
            }
            catch (UnityException)
            {
                return;
            }

            if (!orderChild.CompareTag("Order")) return;

            var ingredientsToBeMerged = new GameObject[orderChild.childCount];
            var ingredientsInChildren = orderChild.GetComponentsInChildren<Transform>();

            for (var i = 0; i < orderChild.childCount; i++)
            {
                ingredientsToBeMerged[i] = ingredientsInChildren[i + 1].gameObject;
            }

            MergeIngredients(ingredientsToBeMerged);
            orderChild.transform.parent = null;
            orderChild.GetComponent<Order>().SayByeBye();
        }
    }
}