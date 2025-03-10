using UnityEditor;
using UnityEngine;

namespace _Scripts
{
    public class AssemblySpot : Surface
    {
        private void OnMouseOver()
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
    }
}
