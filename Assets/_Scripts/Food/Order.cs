using System.Collections.Generic;
using _Scripts.Food.Ingredients._Ingredient;
using _Scripts.Food.Recipes;
using UnityEngine;

namespace _Scripts.Food
{
    public class Order : Surface
    {
        [SerializeField] private RecipesDatabase recipes;
        private readonly List<Ingredient> _ingredients = new();
        private RecipeScriptableObject _matchedRecipe;
        private readonly Dictionary<string, Sprite> _spritesDict = new();
        private bool _isReady;

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
            if (_isReady) return;
            if (!IsOrderComplete()) return;
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Ingredient>().SayByeBye();
            }
            transform.GetComponent<SpriteRenderer>().sprite = _spritesDict[_matchedRecipe.name];
            _isReady = true;
        }
        
        public void MergeIngredients(GameObject[] ingredients)
        {
            if (ingredients.Length == 0) return;

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
            
            _ingredients.Sort();
            for (var i = 0; i < recipe.Ingredients.Count; i++)
            {
                if (_ingredients[i].IngredientName != recipe.Ingredients[i].IngredientName) return false;
            }

            return true;
        }
    }
}