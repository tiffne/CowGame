using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class Burner : Surface
    {
        Ingredient _ingredientHeld = null;
        void OnMouseOver()
        {
            if (Input.GetMouseButtonDown((int)Hands.Left) && _ingredientHeld == null)// && !handLeft.IsEmpty)
            {
                HandLeft.DropItem(this);
            }

            else if (Input.GetMouseButtonDown((int)Hands.Right) && _ingredientHeld == null)// && !handLeft.IsEmpty)
            {
                HandRight.DropItem(this);
            }
        }

        public void Cook(Ingredient ingredient)
        {
            _ingredientHeld = ingredient;
            _ingredientHeld.transform.parent = transform;
            _ingredientHeld.transform.position = transform.position + Vector3.back;
            StartCoroutine(CookingRoutine());
        }

        private IEnumerator CookingRoutine()
        {
            yield return new WaitForSeconds(5);

            _ingredientHeld.IsReady = true;
            _ingredientHeld = null;
        }
    }
}
