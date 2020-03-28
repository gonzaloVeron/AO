using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(typeof(FishingNet), typeof(FishingRod), typeof(Axe), typeof(Pickaxe), typeof(Scissors))]
public abstract class Tool : Equipable
{
    public Tool(string name, int quantity, float weight) : base(name, quantity, weight, 0, 0)
    {
        this.name = name;
        this.quantity = quantity;
        this.weight = weight;
    }
    public abstract Item whatSubstract(FountainOfResources res, int value, int amount);
}
