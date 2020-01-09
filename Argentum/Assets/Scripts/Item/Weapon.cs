using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipable
{
    public Weapon(string name, int minWeapon, int maxWeapon, int quantity, float weight) : base(name, quantity, weight)
    {
        this.weapon = new Tuple<int, int>(minWeapon, maxWeapon);
    }

    public int minWeapon() => this.weapon.item1;

    public int maxWeapon() => this.weapon.item2;

}
