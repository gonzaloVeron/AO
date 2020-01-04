using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public int lifeRegen;
    public float manaRegen;
    public int energyRegen;
    public int hungryRegen;
    public int thirstRegen;
    public int gold;
    public Consumable(string name, int lifeRegen, float manaRegen, int energyRegen, int hungryRegen, int thirstRegen, int gold, int quantity)
    {
        this.name = name;
        this.lifeRegen = lifeRegen;
        this.manaRegen = manaRegen;
        this.energyRegen = energyRegen;
        this.hungryRegen = hungryRegen;
        this.thirstRegen = thirstRegen;
        this.gold = gold;
        this.quantity = quantity;
    }

    public override void Use(Character other)
    {
        other.state.lifePoints += this.lifeRegen;
        other.state.manaPoints += Mathf.RoundToInt(other.state.manaPointsMax * manaRegen);
        other.state.energyPoints += this.energyRegen;
        other.state.hungryPoints += this.hungryRegen;
        other.state.thirstPoints += this.thirstRegen;
        other.gold += this.gold;
        this.quantity -= 1;
    }

    public override Item toDrop(int quantity, bool needRemove, Inventory inv)
    {
        if(needRemove)
        {
            inv.RemoveItem(this);
            return new Consumable(this.name, this.lifeRegen, this.manaRegen, this.energyRegen, this.hungryRegen, this.thirstRegen, this.gold, this.quantity);
        }
        else
        {
            this.quantity -= quantity;
            return new Consumable(this.name, this.lifeRegen, this.manaRegen, this.energyRegen, this.hungryRegen, this.thirstRegen, this.gold, quantity);
        }
    }

}
