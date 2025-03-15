using System.Collections.Generic;
using _Scripts.Food.Ingredients;
using _Scripts.Food.Ingredients._Ingredient;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Food.Recipes
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Scriptable Objects/Recipe")]
    public class RecipeScriptableObject : ScriptableObject
    {
        [SerializeField] private List<Ingredient> ingredients;
        public List<Ingredient> Ingredients => ingredients;

        [SerializeField] private Sprite recipeSprite;
        public Sprite RecipeSprite => recipeSprite;

        public string RecipeName => name;
    }
}