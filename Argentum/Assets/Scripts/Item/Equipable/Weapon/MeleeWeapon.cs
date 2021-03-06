﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public MeleeWeapon(string name, int minWeapon, int maxWeapon, int magicalDefense, int magicalDamage, int quantity, float weight) : base(name, quantity, weight, magicalDefense, magicalDamage)
    {
        this.weapon = new Tuple<int, int>(minWeapon, maxWeapon);
    }
    public override Tuple<int, int> calculateDamage(Player self) => self.weapon.weapon;
    public override float damageMod(Classification clasf) => clasf.meleeDamageMod();
    public override float modForWeapon(Classification clasf) => clasf.meleeAimMod();
    public override int requiredSkill(Skills sk) => sk.armedCombat;
    public override Item copy() => new MeleeWeapon(this.name, this.weapon.item1, this.weapon.item2, this.magicalDefense, this.magicalDamage, this.quantity, this.weight);
}
