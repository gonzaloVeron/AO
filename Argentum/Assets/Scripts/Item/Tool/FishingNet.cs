using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingNet : Tool
{
    public FishingNet(int quantity, float weight) : base("Fishing net", quantity, weight)
    {
        this.quantity = quantity;
        this.weight = weight;
    }
    public override Item copy() => new FishingNet(this.quantity, this.weight);
    public override Item whatSubstract(FountainOfResources res, int value, int amount) => res.extractWithFishingNet(value, amount);
    
}
