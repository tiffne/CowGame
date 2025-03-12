using System;
using UnityEditor;
using UnityEngine;

namespace _Scripts
{
    public class AssemblySpot : Surface
    {
        private CircleCollider2D _circleCollider2D;

        private new void Start()
        {
            base.Start();
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        private void Update()
        {
            _circleCollider2D.enabled = transform.childCount == 0;
        }
    }
}