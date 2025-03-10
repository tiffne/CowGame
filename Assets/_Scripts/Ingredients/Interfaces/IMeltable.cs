using System;
using UnityEngine;

namespace _Scripts.Ingredients.Interfaces
{
    public interface IMeltable
    {
        private float TimeToMelt
        {
            get => TimeToMelt;
            set
            {
                {
                    if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                    TimeToMelt = value;
                }
            }
        }
    }
}