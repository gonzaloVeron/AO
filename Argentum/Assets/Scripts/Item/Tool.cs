using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(typeof(FishingNet), typeof(FishingRod))]
public class Tool : Equipable
{
    public Tool(string name, int quantity, float weight) : base(name, quantity, weight, 0, 0)
    {
        this.name = name;
        this.quantity = quantity;
        this.weight = weight;
    } 
    public override Item copy() => throw new NonCopyableItemException(this.name);
    public virtual Item itemExtracted(int value, int amount) => throw new System.Exception("Algo paso");
}
