using UnityEngine;

namespace _Scripts.Fixed_Surfaces.Openable
{
    public class Fridge : MonoBehaviour
    {
        private SpriteRenderer openFridge;

        void Start()
        {
            openFridge = transform.GetChild(0).GetComponent<SpriteRenderer>();
            openFridge.enabled = false;
        }

        public void OnMouseEnter()
        {
            openFridge.enabled = true;
        }

        public void OnMouseExit()
        {
            openFridge.enabled = false;
        }
    }
}