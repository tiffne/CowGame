using UnityEngine;

namespace _Scripts
{
    public class AssemblySpot : Surface
    {
        void OnMouseDown()
        {
            if (Input.GetMouseButtonDown((int)Hands.Left) && !HandLeft.IsEmpty)
            {
                HandLeft.DropItem(this);

            }
        }
    }
}
