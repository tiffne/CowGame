using UnityEngine;

namespace _Scripts
{
    public class AssemblySpot : Surface
    {
        void OnMouseOver()
        {
            if (Input.GetMouseButtonDown((int)Hands.Left) && !HandLeft.IsEmpty)
            {
                HandLeft.DropItem(this);

            }

            else if (Input.GetMouseButtonDown((int)Hands.Right) && !HandRight.IsEmpty)
            {
                HandRight.DropItem(this);
            }
        }
    }
}
