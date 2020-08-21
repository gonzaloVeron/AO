using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Equipable
{
    public Helmet(string name, int minHelmet, int maxHelmet, int magicalDefense, int magicalDamage, int quantity, float weight) : base(name, quantity, weight, magicalDefense, magicalDamage)
    {
        this.helmet = new Tuple<int, int>(minHelmet, maxHelmet);
    }
    public int minHelmet() => this.helmet.item1;
    public int maxHelmet() => this.helmet.item2;
    public override Item copy() => new Helmet(this.name, this.helmet.item1, this.helmet.item2, this.magicalDefense, this.magicalDamage, this.quantity, this.weight);
    public override void Use(Player other)
    {
        if (other.helmet != this)
        {
            other.helmet = this;
            other.weight += this.weight;
        }
        else
        {
            other.helmet = null;
            other.weight -= this.weight;
        }
    }
}
