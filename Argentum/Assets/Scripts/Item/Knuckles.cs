using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knuckles : Weapon
{
    public Knuckles(string name, int minWeapon, int maxWeapon, int quantity, float weight) : base(name, quantity, weight)
    {
        this.weapon = new Tuple<int, int>(minWeapon, maxWeapon);
    }
    public override float modForWeapon(Classification clasf) => clasf.withoutWeaponAimMod();
    public override int requiredSkill(Skills sk) => sk.martialArts;
}
