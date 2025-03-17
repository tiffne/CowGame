using UnityEngine;

namespace _Scripts.Fixed_Surfaces.Storing
{
    public abstract class Container : Surface
    {
        protected int AmountLeft;
        
        public void ReduceAmountLeft()
        {
            AmountLeft--;
        }
    }
}
