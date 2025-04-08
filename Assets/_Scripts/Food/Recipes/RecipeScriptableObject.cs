using System;
using System.Collections.Generic;
using _Scripts.Food.Ingredients._Ingredient;
using UnityEngine;

namespace _Scripts.Food.Recipes
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Scriptable Objects/Recipe")]
    public class RecipeScriptableObject : ScriptableObject
    {
        public string RecipeName => name;

        [SerializeField] private List<Ingredient> ingredients;
        public List<Ingredient> Ingredients => ingredients;

        [SerializeField] private Sprite recipeSprite;
        public Sprite RecipeSprite => recipeSprite;

        [SerializeField] private float price;
        public float Price => price;
    }
}