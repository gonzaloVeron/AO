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
}
