using _Scripts.Player;
using UnityEngine;

namespace _Scripts
{
    public class Surface : MonoBehaviour
    {
        protected Hand HandLeft;
        protected Hand HandRight;

        protected void Start()
        {
            HandLeft = GameObject.Find("Hand Left").GetComponent<Hand>();
            HandRight = GameObject.Find("Hand Right").GetComponent<Hand>();
        }
        
        protected void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(HandLeft.Index))
            {
                HandLeft.Interact(gameObject);
            }
            else if (Input.GetMouseButtonDown(HandRight.Index))
            {
                HandRight.Interact(gameObject);
            }
        }

        public void SayByeBye()
        {
            Destroy(gameObject);
        }
    }
    

}