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
        other.armor.item1 = this.armor.item1;
        other.armor.item2 = this.armor.item2;
        other.helmet.item1 = this.helmet.item1;
        other.helmet.item2 = this.helmet.item2;
        other.shield.item1 = this.shield.item1;
        other.shield.item2 = this.shield.item2;
        other.weapon.item1 = this.weapon.item1;
        other.weapon.item2 = this.weapon.item2;
    }

    public override Item toDrop(int quantity, bool needRemove, Inventory inv)
    {
        if (needRemove)
        {
            inv.RemoveItem(this);
            return new Equipable(this.name, this.armor.item1, this.armor.item2, this.helmet.item1, this.helmet.item2, this.shield.item1, this.shield.item2, this.weapon.item1, this.weapon.item2, this.quantity);
        }
        else
        {
            this.quantity -= quantity;
            return new Equipable(this.name, this.armor.item1, this.armor.item2, this.helmet.item1, this.helmet.item2, this.shield.item1, this.shield.item2, this.weapon.item1, this.weapon.item2, quantity);
        }
    }

}


