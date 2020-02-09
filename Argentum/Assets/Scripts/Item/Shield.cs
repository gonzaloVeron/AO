using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Equipable
{
    public Shield(string name, int minShield, int maxShield, int quantity, float weight) : base(name, quantity, weight)
    {
        this.shield = new Tuple<int, int>(minShield, maxShield);
    }

    public int minShield() => this.shield.item1;

    public int maxShield() => this.shield.item2;

    public override Item copy() => new Shield(this.name, this.shield.item1, this.shield.item2, this.quantity, this.weight);
}
