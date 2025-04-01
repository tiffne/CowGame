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
        public bool HasTableware { get; private set; }

        private new void Start()
        {
            base.Start();
            foreach (var recipe in recipes.Recipes)
            {
                recipe.Ingredients.Sort();
                _spritesDict[recipe.RecipeName] = recipe.RecipeSprite;
            }
        }

        private void Update()
        {
            if (IsReady) return;
            if (!IsOrderComplete()) return;
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).GetComponent<Ingredient>();
                if (child.gameObject.name.Equals("Plate")) continue;
                child.SayByeBye();
            }

            transform.GetComponent<SpriteRenderer>().sprite = _spritesDict[_matchedRecipe.name];
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