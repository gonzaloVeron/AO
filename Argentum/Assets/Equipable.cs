using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : Item
{

    public Tuple<int, int> armor;
    public Tuple<int, int> helmet;
    public Tuple<int, int> shield;
    public Tuple<int, int> weapon;

    public Equipable(string name, int minArmor, int maxArmor, int minHelmet, int maxHelmet, int minShield, int maxShield, int minWeapon, int maxWeapon, int quantity)
    {
        this.name = name;
        this.armor = new Tuple<int, int>(minArmor, maxArmor);
        this.helmet = new Tuple<int, int>(minHelmet, maxHelmet);
        this.shield = new Tuple<int, int>(minShield, maxShield);
        this.weapon = new Tuple<int, int>(minWeapon, maxWeapon);
        this.quantity = quantity;
    }

    public override void Use(Character other)
    {
        throw new System.NotImplementedException();
    }

}


