using System;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Scripts.Food.Ingredients._Ingredient
{
    public abstract class Ingredient : Surface, IComparable
    {
        [SerializeField] private Order orderPrefab;
        [SerializeField] protected IngredientScriptableObject ingredient;

        private Sprite _ingredientRawSprite;
        private Sprite _ingredientBlendedSprite;
        private Sprite _ingredientCookedSprite;
        private Sprite _ingredientBurnedSprite;
        private Sprite _ingredientMeltedSprite;
        
        public bool CanBlend { get; set; }
        public bool CanCook { get; set; }
        public bool CanMelt { get; set; }

        public float TimeToBlend { get; private set; }
        public float TimeToCook { get; private set; }
        public float TimeToMelt { get; private set; }

        protected bool IsBlended { get; set; } = false;
        protected bool IsCooked { get; set; } = false;
        protected bool IsBurned { get; set; } = false;
        protected bool IsMelted { get; set; } = false;

        private new void Start()
        {
            base.Start();
            tag = "Ingredient";
            CanBlend = ingredient.CanBlend;
            CanCook = ingredient.CanCook;
            CanMelt = ingredient.CanMelt;

            TimeToBlend = ingredient.TimeToBlend;
            TimeToCook = ingredient.TimeToCook;
            TimeToMelt = ingredient.TimeToMelt;

            _ingredientRawSprite = ingredient.IngredientRawSprite;
            _ingredientBlendedSprite = ingredient.IngredientBlendedSprite;
            _ingredientCookedSprite = ingredient.IngredientCookedSprite;
            _ingredientBurnedSprite = ingredient.IngredientBurnedSprite;
            _ingredientMeltedSprite = ingredient.IngredientMeltedSprite;

            GetComponent<SpriteRenderer>().sprite = _ingredientRawSprite;
        }

        private void GenerateNewOrder(GameObject target)
        {
            var order = Instantiate(orderPrefab, transform.parent).gameObject.GetComponent<Order>();
            order.MergeIngredients(new[] { gameObject, target });
        }

        // public void SayByeBye() // TODO: transform this in a method that returns the ingredients to the pantry
        // {
        //     Destroy(gameObject);
        // }

        public int CompareTo(object obj)
        {
            if (obj == null) throw new NullReferenceException("Can't compare null ingredients.");
            var otherIngredient = obj as Ingredient;
            if (otherIngredient)
            {
                return string.CompareOrdinal(name, otherIngredient.name);
            }

            throw new ArgumentException("Object not an Ingredient.");
        }

        private new void OnMouseOver()
        {
            base.OnMouseOver();
            if (transform.childCount == 1) GenerateNewOrder(transform.GetChild(0).gameObject);
        }
    }
}