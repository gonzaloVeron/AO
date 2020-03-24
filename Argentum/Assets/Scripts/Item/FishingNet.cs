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
    public override Item itemExtracted(int value, int amount)
    {
        switch (value)
        {
            case int n when n < 0.2f:
                return new Consumable("Hipocampo", 0, 0, 0, 100, 0, amount, 0f);
            case int n when n < 0.8f:
                return new Consumable("Pez espada", 0, 0, 0, 75, 0, amount, 0f);
            case int n when n < 6.6f:
                return new Consumable("Merluza", 0, 0, 0, 50, 0, amount, 0f);
            case int n when n < 10f:
                return new Consumable("Pargo", 0, 0, 0, 25, 0, amount, 0f);
            default:
                return new Consumable("Cornalito", 0, 0, 0, 5, 0, amount, 0f);
        }
    }
}
