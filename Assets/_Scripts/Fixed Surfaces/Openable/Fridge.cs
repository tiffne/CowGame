using UnityEngine;

namespace _Scripts.Fixed_Surfaces.Openable
{
    public class Fridge : MonoBehaviour
    {
        private SpriteRenderer openFridge;

        [SerializeField] private AudioSource openSound;
        [SerializeField] private AudioSource closeSound;

        void Start()
        {
            openFridge = transform.GetChild(0).GetComponent<SpriteRenderer>();
            openFridge.enabled = false;
        }

        public void OnMouseEnter()
        {
            openSound.Play();
            openFridge.enabled = true;
        }

        public void OnMouseExit()
        {
            closeSound.Play();
            openFridge.enabled = false;
        }
    }
}