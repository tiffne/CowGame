using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : Surface
{
    readonly List<string> recipe = new() { "Bun", "Cheese", "Patty" };
    public List<string> ingredientsInOrder = new();

    void OnMouseDown()
    {
        if (!handLeft.IsEmpty)
        {
            handLeft.DropItem(this);
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
        ingredient.transform.position = transform.position + Vector3.forward + Vector3.up * (ingredientsInOrder.Count - 1);
        if (IsOrderComplete())
        {
            StartCoroutine(CongratulateAndDestroy());
        }
    }

    bool IsOrderComplete()
    {
        if (ingredientsInOrder.Count != recipe.Count)
        {
            return false;
        }

        for (int i = 0; i < recipe.Count; i++)
        {
            if (ingredientsInOrder[i] != recipe[i])
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
