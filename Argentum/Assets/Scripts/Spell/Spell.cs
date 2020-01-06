using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public string name;
    public int minDamage;
    public int maxDamage;
    public int manaPointsNeeded;
    public Spell(string name, int minDamage, int maxDamage, int manaPointsNeeded)
    {
        this.name = name;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.manaPointsNeeded = manaPointsNeeded;
    }
}

public class Healing : Spell
{
    public Healing(string name, int minDamage, int maxDamage, int manaPointsNeeded) : base(name, minDamage, maxDamage, manaPointsNeeded)
    {

    }

}

public class DirectDamage : Spell
{
    public DirectDamage(string name, int minDamage, int maxDamage, int manaPointsNeeded) : base(name, minDamage, maxDamage, manaPointsNeeded)
    {

    }

}

public class ModState : Spell
{
    public ModState(string name, int minDamage, int maxDamage, int manaPointsNeeded) : base(name, minDamage, maxDamage, manaPointsNeeded)
    {

    }

}

public class Invocation : Spell
{
    public Invocation(string name, int minDamage, int maxDamage, int manaPointsNeeded) : base(name, minDamage, maxDamage, manaPointsNeeded)
    {

    }

}
