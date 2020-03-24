using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

[BsonKnownTypes(typeof(Player), typeof(Creature), typeof(Merchant), typeof(FountainOfResources))]
public abstract class Character
{
    public ObjectId _id;
    public string name;
    public State state;

    public Tuple<int, int> hitPoints;
    public abstract void Attack(Character other);
    public abstract int successProbability(Character other);
    public virtual void BeingAttacked(int value)
    {
        this.state.lifePoints = Mathf.Max(0, this.state.lifePoints - Mathf.Max(0, value - this.physicalDefense()));
    }
    public abstract int physicalDefense();
    public abstract float evasion();
}
