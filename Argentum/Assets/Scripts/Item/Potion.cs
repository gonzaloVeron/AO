using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(typeof(RegPotion))]
public class Potion : Consumable
{
    public Potion(string name, int lifeRegen, float manaRegen, int quantity, float weight) : base(name, lifeRegen, manaRegen, 0, 0, 0, quantity, weight) { }

    public override void Use(Player other)
    {
        other.Heal(this.lifeRegen);
        other.HealMana(Mathf.RoundToInt(other.state.maxManaPoints * manaRegen));
    }
}