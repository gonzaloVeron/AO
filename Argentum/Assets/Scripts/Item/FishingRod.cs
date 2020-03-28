using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : Tool
{
    public FishingRod(int quantity, float weight) : base("Fishing rod", quantity, weight)
    {
        this.quantity = quantity;
        this.weight = weight;
    }
    public override Item copy() => new FishingRod(this.quantity, this.weight);

    public override Item whatSubstract(FountainOfResources res, int value, int amount) => res.extractWithFishingRod(value, amount);
}
