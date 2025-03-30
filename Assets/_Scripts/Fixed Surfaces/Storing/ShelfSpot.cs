using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

namespace _Scripts.Fixed_Surfaces.Storing
{
    public class ShelfSpot : Container
    {
        [SerializeField] private GameObject cheesePrefab;
        [SerializeField] private GameObject steakPrefab;
        [SerializeField] private GameObject bunPrefab;
        [SerializeField] private GameObject pattyPrefab;

        [SerializeField] private GameObject platePrefab;
        [SerializeField] private GameObject cupPrefab;

        public GameObject GetRespectiveItem()
        {
            GameObject temp;
            switch (name)
            {
                case "Cheese":
                    temp = Instantiate(cheesePrefab, transform.position, transform.rotation, transform);
                    temp.name = name;
                    return temp;
                case "Steak":
                    temp = Instantiate(steakPrefab, transform.position, transform.rotation, transform);
                    temp.name = name;
                    return temp;
                case "Patty":
                    temp = Instantiate(pattyPrefab, transform.position, transform.rotation, transform);
                    temp.name = name;
                    return temp;
                case "Bun":
                    temp = Instantiate(bunPrefab, transform.position, transform.rotation, transform);
                    temp.name = name;
                    return temp;
                case "Plate":
                    temp = Instantiate(platePrefab, transform.position, transform.rotation, transform);
                    temp.name = name;
                    return temp;
                case "Cup":
                    temp = Instantiate(cupPrefab, transform.position, transform.rotation, transform);
                    temp.name = name;
                    return temp;
                default:
                    return null;
            }
        }
    }
}