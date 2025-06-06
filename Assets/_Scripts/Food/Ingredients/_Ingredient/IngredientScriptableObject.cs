using UnityEngine;

namespace _Scripts.Food.Ingredients._Ingredient
{
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Scriptable Objects/Ingredient")]
    public class IngredientScriptableObject : ScriptableObject
    {
        [SerializeField] private Sprite ingredientRawSprite;
        public Sprite IngredientRawSprite => ingredientRawSprite;

        [SerializeField] private Sprite ingredientBlendedSprite;
        public Sprite IngredientBlendedSprite => ingredientBlendedSprite;

        [SerializeField] private Sprite ingredientCookedSprite;
        public Sprite IngredientCookedSprite => ingredientCookedSprite;

        [SerializeField] private Sprite ingredientBurnedSprite;
        public Sprite IngredientBurnedSprite => ingredientBurnedSprite;

        [SerializeField] private Sprite ingredientMeltedSprite;
        public Sprite IngredientMeltedSprite => ingredientMeltedSprite;
        
        [SerializeField] private bool isReady;
        public bool IsReady => isReady;

        [SerializeField] private bool canBlend;
        public bool CanBlend => canBlend;

        [SerializeField] private bool canCook;
        public bool CanCook => canCook;

        [SerializeField] private int cookAmt;
        public int CookAmt => cookAmt;

        [SerializeField] private bool canMelt;
        public bool CanMelt => canMelt;

        [SerializeField] private float timeToBlend;
        public float TimeToBlend => timeToBlend;

        [SerializeField] private float timeToCook;
        public float TimeToCook => timeToCook;

        [SerializeField] private float timeToMelt;
        public float TimeToMelt => timeToMelt;
    }
}