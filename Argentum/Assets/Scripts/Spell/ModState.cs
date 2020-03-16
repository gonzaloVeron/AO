using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModState : Spell
{
    public ModState(string name, int minDamage, int maxDamage)
    {
        this.name = name;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }
    public override void Effect(Player caster, Player affected)
    {
        affected.ModifyState();
    }
}
