﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipable
{
    public Armor(string name, int minArmor, int maxArmor, int quantity, float weight) : base(name, quantity, weight)
    {
        this.armor = new Tuple<int, int>(minArmor, maxArmor);
    }

    public int minArmor() => this.armor.item1;
    public int maxArmor() => this.armor.item2;
    public override Item copy() => new Armor(this.name, this.armor.item1, this.armor.item2, this.quantity, this.weight);

}
