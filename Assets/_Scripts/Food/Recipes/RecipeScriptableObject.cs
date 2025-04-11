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

        [SerializeField] private Sprite rawASprite;
        public Sprite RawASprite => rawASprite;
        
        [SerializeField] private Sprite burntASprite;
        public Sprite BurntASprite => burntASprite;

        [SerializeField] private Sprite rawBSprite;
        public Sprite RawBSprite => rawBSprite;

        [SerializeField] private Sprite burntBSprite;
        public Sprite BurntBSprite => burntBSprite;

        [SerializeField] private Sprite rawABSprite;
        public Sprite RawABSprite => rawABSprite;

        [SerializeField] private Sprite burntABSprite;
        public Sprite BurntABSprite => burntABSprite;

        [SerializeField] private Sprite rawABurntBSprite;
        public Sprite RawABurntBSprite => rawABurntBSprite;

        [SerializeField] private Sprite burntARawBSprite;
        public Sprite BurntARawBSprite => burntARawBSprite;

        [SerializeField] private float price;
        public float Price => price;
    }
}