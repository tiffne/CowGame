using UnityEngine;

namespace _Scripts.Fixed_Surfaces.Openable
{
    public class Garbage : OpenableContainer
    {

        [SerializeField] private AudioSource trashSound;

        private void Update()
        {
            if (transform.childCount == 0) return;
            trashSound.Play();
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}