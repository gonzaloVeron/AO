using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public string name;
    public int minDamage;
    public int maxDamage;

    public Spell(string name, int minDamage, int maxDamage)
    {
        this.name = name;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }
}
