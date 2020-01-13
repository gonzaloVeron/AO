using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{
    public Dagger(string name, int minWeapon, int maxWeapon, int quantity, float weight) : base(name, quantity, weight)
    {
        this.weapon = new Tuple<int, int>(minWeapon, maxWeapon);
    }
    public override Tuple<int, int> calculateDamage(Character self) => self.weapon.weapon;
    public override float damageMod(Classification clasf) => clasf.meleeDamageMod();
    public override float modForWeapon(Classification clasf) => clasf.meleeAimMod();
    public override void HowToAttack(Character self, Character other)
    {
        if (Random.Range(0f, 101f) <= self.clasf.stabChance(self.skills.stabbing))
        {
            other.BeingAttacked(self.clasf.stabDamage(self.damage()));
        }
        else
        {
            other.BeingAttacked(self.damage());
        }
    }
    public override int requiredSkill(Skills sk) => sk.armedCombat;
}
