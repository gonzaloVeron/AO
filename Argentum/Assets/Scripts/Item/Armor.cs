using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipable
{
    public Armor(string name, int minArmor, int maxArmor, int quantity, float weight) : base(name, quantity, weight)
    {
        this.armor = new Tuple<int, int>(minArmor, maxArmor);
    }

    public override void Use(Character other)
    {
        other.armor = this;
        other.weight += this.weight;
    }

    public int minArmor() => this.armor.item1;

    public int maxArmor() => this.armor.item2;

}
