using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Tool
{
    public Axe(int quantity, float weight) : base("Axe", quantity, weight)
    {
        this.quantity = quantity;
        this.weight = weight;
    }
    public override Item copy() => new Axe(this.quantity, this.weight);
    public override Item itemExtracted(int value, int amount) => new Resource("Madera", amount, 0f);
}
