using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(typeof(DirectDamage), typeof(Healing), typeof(Invocation), typeof(ModState))]
public abstract class Spell
{
    public string name;
    public int minDamage;
    public int maxDamage;
    public int manaPointsNeeded;

    public abstract void Effect(Player caster, Player affected);
}

