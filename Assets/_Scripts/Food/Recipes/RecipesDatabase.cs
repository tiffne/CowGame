using System;
using System.Collections.Generic;
using _Scripts.Food.Ingredients;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Food.Recipes
{
    // [CreateAssetMenu(fileName = "New Recipes Database", menuName = "Scriptable Objects/RecipesDatabase")]
    public class RecipesDatabase : ScriptableObject
    {
        [SerializeField] private List<RecipeScriptableObject> recipes;
        public List<RecipeScriptableObject> Recipes => recipes;
    }
}