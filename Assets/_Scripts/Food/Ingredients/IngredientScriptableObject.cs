using UnityEngine;

namespace _Scripts.Food.Ingredients
{
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Scriptable Objects/Ingredient")]
    public class IngredientScriptableObject : ScriptableObject
    {
        [SerializeField] private Sprite[] ingredientSprites;
        public Sprite[] IngredientSprites => ingredientSprites;

        [SerializeField] private bool canBlend;
        public bool CanBlend => canBlend;
        [SerializeField] private float timeToBlend;
        public float TimeToBlend => timeToBlend;

        [SerializeField] private bool canCook;
        public bool CanCook => canCook;
        [SerializeField] private float timeToCook;
        public float TimeToCook => timeToCook;

        [SerializeField] private bool canMelt;
        public bool CanMelt => canMelt;
        [SerializeField] private float timeToMelt;
        public float TimeToMelt => timeToMelt;
    }
}