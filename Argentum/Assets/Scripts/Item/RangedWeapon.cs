using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public RangedWeapon(string name, int minWeapon, int maxWeapon, int quantity, float weight) : base(name, quantity, weight)
    {
        this.weapon = new Tuple<int, int>(minWeapon, maxWeapon);
    }
    public override float modForWeapon(Classification clasf) => clasf.projectileWeaponAimMod();
    public override int requiredSkill(Skills sk) => sk.projectileWeapons;
}
