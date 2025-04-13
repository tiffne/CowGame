using UnityEngine;

namespace _Scripts.Fixed_Surfaces
{
    public class OpenableContainer : Surface
    {
        [SerializeField] private Sprite closed, open;
        private SpriteRenderer spriteRenderer;

        private new void Start()
        {
            base.Start();
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = closed;
        }

        protected void OnMouseEnter()
        {
            spriteRenderer.sprite = open;
        }

        protected void OnMouseExit()
        {
            spriteRenderer.sprite = closed;
        }
    }
}