using System.Collections;
using _Scripts.Food.Ingredients._Ingredient;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Food.Ingredients.Steak
{
    public class Steak : Ingredient
    {
        [SerializeField] private GameObject pattyPrefab;

        private bool CanCookAgain { get; set; } = true;
        private bool CanBlendAgain { get; set; } = true;

        private const float TimeInApplianceIncrement = 0.1f;


        private Coroutine coroutine;
        private float amountOfTimeCooked;
        private float amountOfTimeBlended;

        private void Update()
        {
            if (!transform.parent) return;
            switch (transform.parent.tag)
            {
                case "Blender":
                    if (CanBlendAgain) coroutine = StartCoroutine(Blend());
                    break;
                case "Burner":
                    if (CanCookAgain) coroutine = StartCoroutine(Cook());
                    break;
                default:
                    CanBlendAgain = true;
                    CanCookAgain = true;
                    if (coroutine != null) StopCoroutine(coroutine);
                    break;
            }
        }

        private IEnumerator Cook()
        {
            CanCookAgain = false;
            CanBlendAgain = false;

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

        private IEnumerator Blend()
        {
            CanBlendAgain = false;

            while (amountOfTimeBlended < TimeToBlend)
            {
                yield return new WaitForSeconds(TimeInApplianceIncrement);
                amountOfTimeBlended += TimeInApplianceIncrement;
            }

            var patty = Instantiate(pattyPrefab, transform.position, transform.rotation, transform.parent);
            patty.name = "Patty";
            SayByeBye();
        }
    }
}