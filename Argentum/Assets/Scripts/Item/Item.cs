using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine.UIElements;

[BsonKnownTypes(typeof(Consumable), typeof(Equipable), typeof(Resource))]
public abstract class Item
{
    public ObjectId _id;

    public string name;

    public int quantity;

    public float weight;

    public virtual void BeTaked(Inventory inv)
    {
        if (inv.existsItem(this.name))
        {
            inv.AddQuantity(this.name, this.quantity);
        }
        else
        {
            this.AddToInv(inv);
        }
    }
    public abstract void Use(Player other);
    public abstract Item toDrop(int quantity, bool needRemove, Inventory inv);
    public bool isEmpty() => this.quantity == 0;
    public abstract Item copy();
    public abstract void AddToInv(Inventory inv);
}

