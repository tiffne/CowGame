using UnityEngine;

public class Surface : MonoBehaviour
{
    protected HandLeft handLeft;
    //[SerializeField] private HandRight handRight;

    protected void Start()
    {
        handLeft = GameObject.Find("Hand Left").GetComponent<HandLeft>();
    }

    protected enum Hands
    {
        Left = 0,
        Right = 1
    }
}
