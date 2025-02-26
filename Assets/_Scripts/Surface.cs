using UnityEngine;


    public class Surface : MonoBehaviour
    {
        protected HandLeft HandLeft;
        //[SerializeField] private HandRight handRight;

        protected void Start()
        {
            HandLeft = GameObject.Find("Hand Left").GetComponent<HandLeft>();
        }

        protected enum Hands
        {
            Left = 0,
            Right = 1
        }
    }

