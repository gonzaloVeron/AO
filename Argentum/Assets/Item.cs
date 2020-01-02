using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;

public abstract class Item
{
    public ObjectId _id;

    public string name;

    public int quantity;

    public abstract void Use(Character other);
    public bool isEmpty()
    {
        return this.quantity == 0;
    }
}

public class Consumible : Item
{
    public int lifeRegen;
    public float manaRegen;
    public int energyRegen;
    public int hungryRegen;
    public int thirstRegen;
    public int gold;
    public Consumible(string name, int lifeRegen, float manaRegen, int energyRegen, int hungryRegen, int thirstRegen, int gold, int quantity)
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

}

public class Equipable : Item
{

    public (int, int) armor;
    public (int, int) helmet;
    public (int, int) shield;
    public (int, int) weapon;

    public Equipable(string name, int minArmor, int maxArmor, int minHelmet, int maxHelmet, int minShield, int maxShield, int minWeapon, int maxWeapon, int quantity)
    {
        this.name = name;
        this.armor.Item1 = minArmor;
        this.armor.Item2 = maxArmor;
        this.helmet.Item1 = minHelmet;
        this.helmet.Item2 = maxHelmet;
        this.shield.Item1 = minShield;
        this.shield.Item2 = maxShield;
        this.weapon.Item1 = minWeapon;
        this.weapon.Item2 = maxWeapon;
        this.quantity = quantity;
    }

    public override void Use(Character other)
    {
        throw new System.NotImplementedException();
    }

}

