using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(typeof(Potion))]
public class Consumable : Item
{
    public int lifeRegen;
    public float manaRegen;
    public int energyRegen;
    public int hungryRegen;
    public int thirstRegen;
    public Consumable(string name, int lifeRegen, float manaRegen, int energyRegen, int hungryRegen, int thirstRegen, int quantity, float weight)
    {
        this.name = name;
        this.lifeRegen = lifeRegen;
        this.manaRegen = manaRegen;
        this.energyRegen = energyRegen;
        this.hungryRegen = hungryRegen;
        this.thirstRegen = thirstRegen;
        this.quantity = quantity;
        this.weight = weight;
    }

    public override void Use(Player other)
    {
        other.state.energyPoints += this.energyRegen;
        other.state.hungryPoints += this.hungryRegen;
        other.state.thirstPoints += this.thirstRegen;
        this.quantity -= 1;
        if (this.isEmpty())
        {
            other.inv.RemoveItem(this);
        }
    }

    public override Item toDrop(int quantity, bool needRemove, Inventory inv)
    {
        if(needRemove)
        {
            inv.RemoveItem(this);
            return new Consumable(this.name, this.lifeRegen, this.manaRegen, this.energyRegen, this.hungryRegen, this.thirstRegen, this.quantity, this.weight);
        }
        else
        {
            this.quantity -= quantity;
            return new Consumable(this.name, this.lifeRegen, this.manaRegen, this.energyRegen, this.hungryRegen, this.thirstRegen, quantity, this.weight);
        }
    }
    public override Item copy() => new Consumable(this.name, this.lifeRegen, this.manaRegen, this.energyRegen, this.hungryRegen, this.thirstRegen, this.quantity, this.weight);

}
