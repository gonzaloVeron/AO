using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invocation : Spell
{
    public Invocation(string name, int minDamage, int maxDamage, int manaPointsNeeded)
    {
        this.name = name;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.manaPointsNeeded = manaPointsNeeded;
    }
    public override void Effect(Player caster, Player affected)
    {

    }
}
