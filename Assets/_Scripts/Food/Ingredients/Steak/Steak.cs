using System.Collections;
using _Scripts.Food.Ingredients._Ingredient;
using UnityEngine;

namespace _Scripts.Food.Ingredients.Steak
{
    public class Steak : Ingredient
    {
        public bool IsCooking { get; set; }
        private bool CanCookAgain { get; set; } = true;
        private float timerStart;
        private Coroutine coroutine;
        private float i;


        private void Update()
        {
            if (!IsCooking) return;
            if (CanCookAgain)
            {
                coroutine = StartCoroutine(Cook());
            }
            else if (!transform.parent.CompareTag("Burner"))
            {
                IsCooking = false;
                CanCookAgain = true;
                StopCoroutine(coroutine);
            }
        }

        private IEnumerator Cook()
        {
            CanCookAgain = false;
            while (i < TimeToCook)
            {
                yield return new WaitForSeconds(1.0f);
                i++;
            }

            switch (CurrentState)
            {
                case State.Raw:
                    CurrentState = State.Cooked;
                    IsReady = true;
                    SpriteRenderer.sprite = ingredient.IngredientCookedSprite;
                    break;
                case State.Cooked:
                    CurrentState = State.Burned;
                    IsReady = false;
                    SpriteRenderer.sprite = ingredient.IngredientBurnedSprite;
                    break;
            }

            i = 0.0f;

            CanCookAgain = true;
        }
        
        private IEnumerator Blend()
        {
            yield return null;
        }
    }
}