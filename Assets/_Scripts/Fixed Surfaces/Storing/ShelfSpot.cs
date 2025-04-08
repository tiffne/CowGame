using _Scripts.Fixed_Surfaces.Openable;
using Unity.VisualScripting;
// using UnityEditorInternal;
using UnityEngine;

namespace _Scripts.Fixed_Surfaces.Storing
{
    public class ShelfSpot : Container
    {
        [SerializeField] private GameObject cheesePrefab;
        [SerializeField] private GameObject steakPrefab;
        [SerializeField] private GameObject bunPrefab;
        [SerializeField] private GameObject pattyPrefab;
        [SerializeField] private GameObject iceCreamPrefab;
        [SerializeField] private GameObject friesPrefab;

        [SerializeField] private GameObject platePrefab;
        [SerializeField] private GameObject cupPrefab;

        private new void OnMouseOver()
        {
            if (transform.parent.TryGetComponent<Fridge>(out var container)) container.OnMouseEnter();
            base.OnMouseOver();
        }

        public GameObject GetRespectiveItem()
        {
            GameObject temp;
            switch (name)
            {
                case "Cheeses":
                    temp = Instantiate(cheesePrefab, transform.position, transform.rotation, transform);
                    temp.name = "Cheese";
                    return temp;
                case "Steaks":
                    temp = Instantiate(steakPrefab, transform.position, transform.rotation, transform);
                    temp.name = "Steak";
                    return temp;
                case "Buns":
                    temp = Instantiate(bunPrefab, transform.position, transform.rotation, transform);
                    temp.name = "Bun";
                    return temp;
                case "IceCreams":
                    temp = Instantiate(iceCreamPrefab, transform.position, transform.rotation, transform);
                    temp.name = "IceCream";
                    return temp;
                case "FryBag":
                    temp = Instantiate(friesPrefab, transform.position, transform.rotation, transform);
                    temp.name = "Fries";
                    return temp;
                case "Plates":
                    temp = Instantiate(platePrefab, transform.position, transform.rotation, transform);
                    temp.name = "Plate";
                    return temp;
                case "Cups":
                    temp = Instantiate(cupPrefab, transform.position, transform.rotation, transform);
                    temp.name = "Cup";
                    return temp;
                default:
                    return null;
            }
        }
    }
}