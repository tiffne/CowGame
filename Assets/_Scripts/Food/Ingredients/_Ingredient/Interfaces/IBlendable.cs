using System;

namespace _Scripts.Food.Ingredients._Ingredient.Interfaces
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
