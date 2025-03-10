using _Scripts.Player;
using UnityEngine;

namespace _Scripts
{
    public class Surface : MonoBehaviour
    {
        protected Hand HandLeft, HandRight;

        private void Start()
        {
            HandLeft = GameObject.Find("Hand Left").GetComponent<Hand>();
            HandRight = GameObject.Find("Hand Right").GetComponent<Hand>();
        }
    }
}