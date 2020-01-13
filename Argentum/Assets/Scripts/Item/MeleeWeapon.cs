using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public MeleeWeapon(string name, int minWeapon, int maxWeapon, int quantity, float weight) : base(name, quantity, weight)
    {
        this.weapon = new Tuple<int, int>(minWeapon, maxWeapon);
    }
    public override float modForWeapon(Classification clasf) => clasf.meleeAimMod();

    public override int requiredSkill(Skills sk) => sk.armedCombat;
}
