using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{
    public Dagger(string name, int minWeapon, int maxWeapon, int quantity, float weight) : base(name, minWeapon, maxWeapon, quantity, weight) { }
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
}
