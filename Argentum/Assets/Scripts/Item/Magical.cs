﻿using System.Collections;
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
}
