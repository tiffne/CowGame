using System;

namespace _Scripts.Ingredients.Interfaces
{
    public interface ICookable
    {
        private float TimeInBurner
        {
            get => TimeInBurner;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                TimeInBurner = value;
            }
        }
    
        // Implement IEnumerator for cooking
    }
}
