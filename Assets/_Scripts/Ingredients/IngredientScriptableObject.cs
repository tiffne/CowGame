using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Ingredients
{
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Scriptable Objects/Ingredient")]
    public class IngredientScriptableObject : ScriptableObject
    {
        [SerializeField] private string ingredientName;
        public string IngredientName => ingredientName;

        [SerializeField] private Sprite ingredientSprite;
        public Sprite IngredientSprite => ingredientSprite;

        [SerializeField] private bool goesInBlender;
        public bool GoesInBlender => goesInBlender;

        [SerializeField] private bool goesInBurner;
        public bool GoesInBurner => goesInBurner;
    }
}