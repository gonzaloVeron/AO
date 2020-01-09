using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamage : Spell
{
    public DirectDamage(string name, int minDamage, int maxDamage, int manaPointsNeeded)
    {
        this.name = name;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.manaPointsNeeded = manaPointsNeeded;
    }
    public override void Effect(Character caster, Character affected)
    {
        affected.BeAttackedWithMagic(caster.magicDamage(this.minDamage, this.maxDamage, caster.extraMagicDamage()));
    }
}
