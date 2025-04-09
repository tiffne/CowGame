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

        public bool IsReady { get; protected set; }

        private Sprite _ingredientRawSprite;
        private Sprite _ingredientBlendedSprite;
        private Sprite _ingredientCookedSprite;
        private Sprite _ingredientBurnedSprite;
        private Sprite _ingredientMeltedSprite;

        public bool CanBlend { get; protected set; }
        public bool CanCook { get; private set; }
        public bool CanMelt { get; private set; }

        protected float TimeToBlend { get; private set; }
        protected float TimeToCook { get; private set; }
        protected float TimeToMelt { get; private set; }

        protected bool IsBlended { get; set; } = false;
        protected bool IsCooked { get; set; } = false;
        protected bool IsBurned { get; set; } = false;
        protected bool IsMelted { get; set; } = false;

        protected SpriteRenderer SpriteRenderer;

        protected enum State
        {
            Raw,
            Cooked,
            Burned,
            Melted,
            Blended,
        }

        protected Enum CurrentState;


        private new void Start()
        {
            base.Start();
            tag = "Ingredient";
            CanBlend = ingredient.CanBlend;
            CanCook = ingredient.CanCook;
            CanMelt = ingredient.CanMelt;
            IsReady = ingredient.IsReady;


            TimeToBlend = ingredient.TimeToBlend >= 0
                ? ingredient.TimeToBlend
                : throw new ArgumentException("Time to Blend can't be negative");
            TimeToCook = ingredient.TimeToCook >= 0
                ? ingredient.TimeToCook
                : throw new ArgumentException("Time to Cook can't be negative");
            TimeToMelt = ingredient.TimeToMelt >= 0
                ? ingredient.TimeToMelt
                : throw new ArgumentException("TimeToMelt can't be negative");

            _ingredientRawSprite = ingredient.IngredientRawSprite;
            _ingredientBlendedSprite = ingredient.IngredientBlendedSprite;
            _ingredientCookedSprite = ingredient.IngredientCookedSprite;
            _ingredientBurnedSprite = ingredient.IngredientBurnedSprite;
            _ingredientMeltedSprite = ingredient.IngredientMeltedSprite;

            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.sprite = _ingredientRawSprite;
            CurrentState = State.Raw;
        }

        public void GenerateNewOrder(GameObject target)
        {
            var ingsToMerge = target ? new[] { gameObject, target } : new[] { gameObject };
            var order = Instantiate(orderPrefab, transform.parent).gameObject.GetComponent<Order>();
            order.MergeIngredients(ingsToMerge);
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