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

