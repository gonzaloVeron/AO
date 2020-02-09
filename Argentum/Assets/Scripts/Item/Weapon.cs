using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Equipable
{
    public Weapon(string name, int quantity, float weight) : base(name, quantity, weight) { }
    public int minWeapon() => this.weapon.item1;
    public int maxWeapon() => this.weapon.item2;
    public abstract float modForWeapon(Classification clasf);
    public abstract float damageMod(Classification clasf);
    public abstract Tuple<int, int> calculateDamage(Character self);
    public virtual void HowToAttack(Character self, Character other)
    {
        self.clasf.Attack(self, other);
    }
    public abstract int requiredSkill(Skills sk);

}
