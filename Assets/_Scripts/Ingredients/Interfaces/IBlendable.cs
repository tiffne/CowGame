using System;
using UnityEngine;

namespace _Scripts.Ingredients.Interfaces
{
    public interface IBlendable
    {
        private float TimeInBlender
        {
            get => TimeInBlender;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                TimeInBlender = value;
            }
        }
    }
}
