using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Equipable
{
    public Tuple<int, int> damage;

    public Arrow(string name, int minDamage, int maxDamage, int quantity, float weight) : base(name, quantity, weight, 0, 0)
    {
        this.damage = new Tuple<int, int>(minDamage, maxDamage);
    }
    public override Item copy() => new Arrow(this.name, this.damage.item1, this.damage.item2, this.quantity, this.weight);
    public override void Use(Player other)
    {
        if (other.arrow != this)
        {
            other.arrow = this;
            other.weight += this.weight;
        }
        else
        {
            other.arrow = NoArrow.Instance;
            other.weight -= this.weight;
        }
    }

    public int minDamage() => this.damage.item1;

    public int maxDamage() => this.damage.item2;

    public virtual bool isArrow() => true;
}
