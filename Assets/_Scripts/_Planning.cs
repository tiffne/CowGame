using UnityEngine;

public class _Planning : MonoBehaviour
{

    /*
    Pockets have Ingredients
    Ingredients go to hand or surface or pans or on other ingredients
        Ingredients to hand - hand picks them up
        Ingredents to surface - If ingredient is ready, instantiate new order object that has that ingredient,
                                move original ingredient back to pocket
                                Otherwise, just drop them
        Ingredients on other Ingredients == Ingredients on Order - same as Ingredient to Surface
    */
}
