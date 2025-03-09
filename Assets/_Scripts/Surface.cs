using UnityEngine;


    public class Surface : MonoBehaviour
    {
        protected HandLeft HandLeft;
        protected HandRight HandRight;

        protected void Start()
        {
            HandLeft = GameObject.Find("Hand Left").GetComponent<HandLeft>();
            HandRight = GameObject.Find("Hand Right").GetComponent<HandRight>();
    }

        protected enum Hands
        {
            Left = 0,
            Right = 1
        }
    }

