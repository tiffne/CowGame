using System.Collections;
using _Scripts.Food.Ingredients._Ingredient;
using UnityEngine;

namespace _Scripts.Food.Ingredients.Steak
{
    public class Steak : Ingredient
    {
        private bool CanCookAgain { get; set; } = true;
        private float timerStart;
        private Coroutine coroutine;
        private float amountOfTimeCooked;


        private void Update()
        {
            //if (!IsCooking) return;
            if (CanCookAgain && transform.parent.CompareTag("Burner"))
            {
                coroutine = StartCoroutine(Cook());
            }
            else if (!transform.parent.CompareTag("Burner"))
            {
                CanCookAgain = true;
                if (coroutine != null) StopCoroutine(coroutine);
            }
        }

        private IEnumerator Cook()
        {
            CanCookAgain = false;
            Debug.Log(amountOfTimeCooked);

            while (amountOfTimeCooked < TimeToCook)
            {
                yield return new WaitForSeconds(1.0f);
                amountOfTimeCooked++;
                Debug.Log(amountOfTimeCooked);

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

        private IEnumerator Blend()
        {
            yield return null;
        }
    }
}