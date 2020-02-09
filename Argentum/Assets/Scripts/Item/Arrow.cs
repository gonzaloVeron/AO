using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Equipable
{
    public Tuple<int, int> damage;

    public Arrow(string name, int minDamage, int maxDamage, int quantity, float weight) : base(name, quantity, weight)
    {
        this.damage = new Tuple<int, int>(minDamage, maxDamage);
    }
    public override Item copy() => new Arrow(this.name, this.damage.item1, this.damage.item2, this.quantity, this.weight);
}
