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
    public override Item itemExtracted(int value, int amount) => new Consumable("Cornalito", 0, 0, 0, 5, 0, amount, 0f);
}
