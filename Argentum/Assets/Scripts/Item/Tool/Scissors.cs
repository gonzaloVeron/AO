using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : Tool
{
    public Scissors(int quantity, float weight) : base("Scissors", quantity, weight)
    {
        this.quantity = quantity;
        this.weight = weight;
    }
    public override Item copy() => new Scissors(this.quantity, this.weight);

    public override Item whatSubstract(FountainOfResources res, int value, int amount) => res.extractWithScissors(value, amount);
}
