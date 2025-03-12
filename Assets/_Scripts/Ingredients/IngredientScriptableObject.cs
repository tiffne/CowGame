using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Ingredients
{
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Scriptable Objects/Ingredient")]
    public class IngredientScriptableObject : ScriptableObject
    {
        [SerializeField] private Sprite[] ingredientSprites;
        public Sprite[] IngredientSprites => ingredientSprites;

        [SerializeField] private bool goesInBlender;
        public bool GoesInBlender => goesInBlender;

        [SerializeField] private bool goesInBurner;
        public bool GoesInBurner => goesInBurner;
    }
}