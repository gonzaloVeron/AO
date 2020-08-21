using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magical : Equipable
{   
    public Magical(string name, int magicalDefense, int magicalDamage, int quantity, float weight) : base (name, quantity, weight, magicalDefense, magicalDamage)
    {
        this.magicalDefense = magicalDefense;
        this.magicalDamage = magicalDamage;
    }

    public override Item copy() => new Magical(this.name, this.magicalDefense, this.magicalDamage, this.quantity, this.weight);
    public override void Use(Player other)
    {
        if (!other.magicalItemsEquiped.exists(s => s.name == this.name))
        {
            other.magicalItemsEquiped.Add(this);
            other.weight += this.weight;
        }
        else
        {
            other.magicalItemsEquiped.Remove(this);
            other.weight -= this.weight;
        }
    }
}
