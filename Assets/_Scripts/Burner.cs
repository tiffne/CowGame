using System.Collections;
using UnityEngine;

public class Burner : Surface
{
    Ingredient ingredientHeld = null;
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown((int)Hands.Left) && ingredientHeld == null)// && !handLeft.IsEmpty)
        {
            handLeft.DropItem(this);
        }
    }

    public void Cook(Ingredient ingredient)
    {
        ingredientHeld = ingredient;
        ingredientHeld.transform.parent = transform;
        ingredientHeld.transform.position = transform.position + Vector3.back;
        StartCoroutine(CookingRoutine());
    }

    private IEnumerator CookingRoutine()
    {
        yield return new WaitForSeconds(5);

        ingredientHeld.IsReady = true;
        ingredientHeld = null;
    }
}
