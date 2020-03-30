using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public RangedWeapon(string name, int minWeapon, int maxWeapon, int quantity, float weight) : base(name, quantity, weight, 0, 0)
    {
        this.weapon = new Tuple<int, int>(minWeapon, maxWeapon);
    }
    public override Tuple<int, int> calculateDamage(Player self) => new Tuple<int, int>(self.weapon.weapon.item1 + self.arrow.damage.item1, self.weapon.weapon.item2 + self.arrow.damage.item2);
    public override float damageMod(Classification clasf) => clasf.projectileWeaponDamageMod();
    public override float modForWeapon(Classification clasf) => clasf.projectileWeaponAimMod();
    public override int requiredSkill(Skills sk) => sk.projectileWeapons;
    public override void HowToAttack(Player self, Character other)
    {
        if (self.hasAmmunition())
        {
            self.clasf.AttackWithBow(self, other);
            self.DiscardAmmunition();
        }
        else
        {
            throw new WithoutAmmunitionException(self.name);
        }
    }
    public override Item copy() => new RangedWeapon(this.name, this.weapon.item1, this.weapon.item2, this.quantity, this.weight);
}
