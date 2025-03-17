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

        private new void Start()
        {
            AmountLeft = 2;
        }

        private void Update()
        {
            if (transform.childCount != 0) return;

            if (AmountLeft > 0) PopulateShelf();
            else Destroy(gameObject);
        }

        private void PopulateShelf()
        {
            GameObject temp;
            switch (tag)
            {
                case "Cheese":
                    temp = Instantiate(cheesePrefab, transform.position, transform.rotation, transform);
                    temp.name = tag;
                    break;
                case "Steak":
                    temp = Instantiate(steakPrefab, transform.position, transform.rotation, transform);
                    temp.name = tag;
                    break;
                case "Patty":
                    temp = Instantiate(pattyPrefab, transform.position, transform.rotation, transform);
                    temp.name = tag;
                    break;
                case "Bun": //TODO Need to fill out the rest of the pantry with more ingredients 
                    break;
            }
        }
    }
}