using System;
using System.Collections;
using TreeEditor;
using UnityEngine;

namespace _Scripts.Player
{
    public class Inventory : MonoBehaviour
    {
        private const int InventoryMaxY = -4;
        private const float InventoryMinY = -5.3f;
        private bool movingUp;

        private const float InventoryMoveSpeed = 6.0f;

        private void Update()
        {
            if (movingUp) return;
            transform.Translate(InventoryMoveSpeed / 2* Time.deltaTime * Vector3.down);
            if (transform.position.y <= InventoryMinY)
            {
                transform.position = new Vector3(transform.position.x, InventoryMinY, transform.position.z);
            }
        }

        public void OnMouseOver()
        {
            movingUp = true;
            transform.Translate(InventoryMoveSpeed * Time.deltaTime * Vector3.up);
            if (transform.position.y >= InventoryMaxY)
            {
                transform.position = new Vector3(transform.position.x, InventoryMaxY, transform.position.z);
            }
        }

        private void OnMouseExit()
        {
            movingUp = false;
        }
    }
}