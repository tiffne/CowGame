using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : Surface
{
    private readonly List<string> _recipe = new() { "Bun", "Cheese", "Patty" };
    public List<string> ingredientsInOrder = new();

    void OnMouseDown()
    {
        if (!HandLeft.IsEmpty)
        {
            HandLeft.DropItem(this);
        }
    }

    public void CombineIngredient(Ingredient ingredient)
    {
        if (!ingredient.IsReady)
        {
            return;
        }

        ingredientsInOrder.Add(ingredient.tag);
        ingredientsInOrder.Sort();
        ingredient.GetComponent<Collider2D>().enabled = false;
        ingredient.transform.parent = transform;
        ingredient.transform.position =
            transform.position + Vector3.forward + Vector3.up * (ingredientsInOrder.Count - 1);
        if (IsOrderComplete())
        {
            StartCoroutine(CongratulateAndDestroy());
        }
    }

    bool IsOrderComplete()
    {
        if (ingredientsInOrder.Count != _recipe.Count)
        {
            return false;
        }

        for (var i = 0; i < _recipe.Count; i++)
        {
            if (ingredientsInOrder[i] != _recipe[i])
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator CongratulateAndDestroy()
    {
        Debug.Log("Order Done!");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}