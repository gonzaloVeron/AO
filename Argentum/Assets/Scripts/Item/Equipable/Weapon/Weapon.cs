﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(typeof(Knuckles), typeof(Dagger), typeof(RangedWeapon), typeof(MeleeWeapon))]
public abstract class Weapon : Equipable
{
    public Weapon(string name, int quantity, float weight, int magicalDefense, int magicalDamage) : base(name, quantity, weight, magicalDefense, magicalDamage) { }
    public int minWeapon() => this.weapon.item1;
    public int maxWeapon() => this.weapon.item2;
    public abstract float modForWeapon(Classification clasf);
    public abstract float damageMod(Classification clasf);
    public abstract Tuple<int, int> calculateDamage(Player self);
    public virtual void HowToAttack(Player self, Character other)
    {
        self.clasf.Attack(self, other);
    }
    public abstract int requiredSkill(Skills sk);
    public override void Use(Player other)
    {
        if (other.weapon != this)
        {
            other.weapon = this;
            other.weight += this.weight;
        }
        else
        {
            other.weapon = NoWeapon.Instance;
            other.weight -= this.weight;
        }
    }
}
