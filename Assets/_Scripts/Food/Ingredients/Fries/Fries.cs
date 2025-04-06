using System.Collections;
using _Scripts.Food.Ingredients._Ingredient;
using UnityEngine;

namespace _Scripts.Food.Ingredients.Patty
{
    public class Fries : Ingredient
    {
        private bool CanCookAgain { get; set; } = true;

        private const float TimeInApplianceIncrement = 0.1f;


        private Coroutine coroutine;
        private float amountOfTimeCooked;


        private void Update()
        {
            if (!transform.parent) return;
            switch (transform.parent.tag)
            {
                case "Burner":
                    if (CanCookAgain) coroutine = StartCoroutine(Cook());
                    break;
                default:
                    CanCookAgain = true;
                    if (coroutine != null) StopCoroutine(coroutine);
                    break;
            }
        }

        private IEnumerator Cook()
        {
            CanCookAgain = false;
            while (amountOfTimeCooked < TimeToCook)
            {
                yield return new WaitForSeconds(TimeInApplianceIncrement);
                amountOfTimeCooked += TimeInApplianceIncrement;
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

            amountOfTimeCooked = 0.0f;

            CanCookAgain = true;
        }
    }
}
