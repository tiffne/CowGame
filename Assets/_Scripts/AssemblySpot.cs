using UnityEngine;

public class AssemblySpot : Surface
{
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown((int)Hands.Left) && !handLeft.IsEmpty)
        {
            handLeft.DropItem(this);

        }
    }
}
