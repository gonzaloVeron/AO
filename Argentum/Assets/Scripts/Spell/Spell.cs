using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public string name;
    public int minDamage;
    public int maxDamage;
    public int manaPointsNeeded;

    public abstract void Effect(Character caster, Character affected);
}

public class Healing : Spell
{
    public Healing(string name, int minDamage, int maxDamage, int manaPointsNeeded)
    {
        this.name = name;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.manaPointsNeeded = manaPointsNeeded;
    }

    public override void Effect(Character caster, Character affected)
    {
        affected.Heal(caster.magicDamage(this.minDamage, this.maxDamage, caster.extraMagicDamage()));
    }

}

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

public abstract class ModState : Spell
{
    public ModState(string name, int minDamage, int maxDamage)
    {
        this.name = name;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }
    public override void Effect(Character caster, Character affected)
    {
        affected.ModifyState();
    }
}

public class Invocation : Spell
{
    public Invocation(string name, int minDamage, int maxDamage, int manaPointsNeeded)
    {
        this.name = name;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.manaPointsNeeded = manaPointsNeeded;
    }
    public override void Effect(Character caster, Character affected)
    {

    }

}
