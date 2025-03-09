using _Scripts;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Surface itemInHand = null;
    [SerializeField] bool _isEmpty = true;
    [SerializeField] GameObject orderPrefab;
    Order order;
    public bool IsEmpty
    {
        get { return _isEmpty; }
    }

    public void GrabItem(Surface item)
    {
        if (IsEmpty)
        {
            itemInHand = item;
            itemInHand.transform.parent = transform;
            itemInHand.transform.position = transform.position;
            _isEmpty = false;
        }
    }

    public void DropItem(Surface surface)
    {
        if (!IsEmpty)
        {
            if (itemInHand.TryGetComponent(out Ingredient ingredient))
            {
                if (surface.TryGetComponent(out AssemblySpot assemblySpot))
                {
                    order = Instantiate(orderPrefab).GetComponent<Order>();
                    order.transform.position = assemblySpot.transform.position + Vector3.back;
                    order.CombineIngredient(ingredient);
                }
                else if (surface.TryGetComponent(out Order clickedOrder) && ingredient.IsReady)
                {
                    clickedOrder.CombineIngredient(ingredient);
                }
                else if (surface.TryGetComponent(out Burner burner) && !ingredient.IsReady)
                {
                    burner.Cook(ingredient);
                }
                else
                {
                    return;
                }
            }
            itemInHand = null;
            _isEmpty = true;
        }
    }
}
