using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magical : Equipable
{
    public Tuple<int, int> magicalDefense;

    public Tuple<int, int> magicalDamage;
    
    public  Magical(string name, int quantity, float weight, int minMagicalDefense, int maxMagicalDefense, int minMagicalDamage, int maxMagicalDamage) : base (name, quantity, weight)
    {
        this.magicalDefense = new Tuple<int, int>(minMagicalDefense, maxMagicalDefense);
        this.magicalDamage = new Tuple<int, int>(minMagicalDamage, maxMagicalDamage);
    }

    public override void Use(Character other)
    {
        other.EquipMagicalItem(this);
    }

}
