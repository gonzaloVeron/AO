using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : Item
{

    public Tuple<int, int> armor;
    public Tuple<int, int> helmet;
    public Tuple<int, int> shield;
    public Tuple<int, int> weapon;
    public int magicalDefense;
    public int magicalDamage;

    public Equipable(string name, int quantity, float weight, int magicalDefense, int magicalDamage)
    {
        this.name = name;
        this.quantity = quantity;
        this.weight = weight;
        this.magicalDefense = magicalDefense;
        this.magicalDamage = magicalDamage;
    }
    public override void Use(Player other)
    {
        if (other.isEquiped(this))
        {
            other.UnequipItem(this);
        }
        else
        {
            other.EquipItem(this);
        }
    }
    public override Item toDrop(int quantity, bool needRemove, Inventory inv)
    {
        if (needRemove)
        {
            inv.RemoveItem(this);
            return new Equipable(this.name, this.quantity, this.weight, this.magicalDefense, this.magicalDamage);
        }
        else
        {
            this.quantity -= quantity;
            return new Equipable(this.name, quantity, this.weight, this.magicalDefense, this.magicalDamage);
        }
    }
    public override Item copy()
    {
        throw new NonCopyableItemException(this.name);
    }
}


