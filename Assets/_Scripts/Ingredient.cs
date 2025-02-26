using UnityEngine;


    public class Ingredient : Surface
    {
        bool _isReady = true;
        public bool IsReady
        {
            get { return _isReady; }
            set
            {
                _isReady = value;
                if (IsReady && transform.CompareTag("Patty") && TryGetComponent(out SpriteRenderer sprite))
                {
                    sprite.sprite = cookedPatty;
                }
            }
        }

        float _cookingTime;
        public float CookingTime
        {
            get { return _cookingTime; }
        }

        public Sprite cookedPatty;
        private SpriteRenderer spriteRenderer; 

        new void Start()
        {
            base.Start();
            if (transform.CompareTag("Patty"))
            {
                IsReady = false;
                _cookingTime = 5.0f;
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            else
            {
                IsReady = true;
            }
        }

        public void OnMouseDown()
        {
            if (HandLeft.IsEmpty)
            {
                HandLeft.GrabItem(this);
            }
            else
            {
                HandLeft.DropItem(this);
            }
        }

        public void ReturnToPocket()
        {
            switch (gameObject.tag)
            {
                case "Bun":
                    transform.position = new Vector3(1, -3, -1);
                    break;
                case "Cheese":
                    transform.position = new Vector3(-1, -3, -1);
                    break;
                case "Patty":
                    transform.position = new Vector3(-3, -3, -1);
                    break;
            }
        }
    }
