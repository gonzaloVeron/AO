using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Tool
{
    public Pickaxe(int quantity, float weight) : base("Pickaxe", quantity, weight)
    {
        this.quantity = quantity;
        this.weight = weight;
    }
    public override Item copy() => new Pickaxe(this.quantity, this.weight);

    public override Item whatSubstract(FountainOfResources res, int value, int amount) => res.extractWithPickaxe(value, amount);
}
