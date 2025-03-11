using System;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts
{
    public class Surface : MonoBehaviour
    {
        private Hand _handLeft, _handRight;

        protected void Start()
        {
            _handLeft = GameObject.Find("Hand Left").GetComponent<Hand>();
            _handRight = GameObject.Find("Hand Right").GetComponent<Hand>();
        }
        
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(_handLeft.Index))
            {
                _handLeft.Interact(gameObject);
            }
            else if (Input.GetMouseButtonDown(_handRight.Index))
            {
                _handRight.Interact(gameObject);
            }
        }
    }
    

}