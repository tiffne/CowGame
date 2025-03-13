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

        [SerializeField] private bool canBlend;
        public bool CanBlend => canBlend;

        [SerializeField] private bool canCook;
        public bool CanCook => canCook;

        [SerializeField] private bool canMelt;
        public bool CanMelt => canMelt;
    }
}