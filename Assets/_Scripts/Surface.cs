using _Scripts.Player;
using UnityEngine;

namespace _Scripts
{
    public class Surface : MonoBehaviour
    {
        private Hand handLeft;
        private Hand handRight;

        protected void Start()
        {
            handLeft = GameObject.Find("Hand Left").GetComponent<Hand>();
            handRight = GameObject.Find("Hand Right").GetComponent<Hand>();
        }
        
        protected void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(handLeft.Index))
            {
                handLeft.Interact(gameObject);
            }
            else if (Input.GetMouseButtonDown(handRight.Index))
            {
                handRight.Interact(gameObject);
            }
        }

        public void SayByeBye()
        {
            Destroy(gameObject);
        }
    }
    

}