using UnityEngine;

public class Pocket : MonoBehaviour
{
    Ingredient ingredientHeld;
    [SerializeField] GameObject pattyPrefab;
    [SerializeField] GameObject cheesePrefab;
    [SerializeField] GameObject bunPrefab;


    void Update()
    {
        if (transform.childCount == 0)
        {
            InstantiateIngredients();
        }
    }

    void InstantiateIngredients()
    {
        switch (tag)
        {
            case "Patty":
                Debug.Log("Patty");
                ingredientHeld = Instantiate(pattyPrefab).GetComponent<Ingredient>();
                break;
            case "Cheese":
                Debug.Log("Cheese");
                ingredientHeld = Instantiate(cheesePrefab).GetComponent<Ingredient>();
                break;
            case "Bun":
                Debug.Log("Bun");
                ingredientHeld = Instantiate(bunPrefab).GetComponent<Ingredient>();
                break;
        }
        ingredientHeld.transform.parent = transform;
        ingredientHeld.transform.position = transform.position;
    }
}
