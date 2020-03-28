using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Tool
{
    public Hammer(int quantity, float weight) : base("Hammer", quantity, weight)
    {
        this.quantity = quantity;
        this.weight = weight;
    }
    public override Item whatSubstract(FountainOfResources res, int value, int amount) => res.extractWithAxe(0, 0); //Esto es para que rompa, no tiene que hacer nada
}
