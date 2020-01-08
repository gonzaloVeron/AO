using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;

public abstract class Item
{
    public ObjectId _id;

    public string name;

    public int quantity;

    public float weight;

    public abstract void Use(Character other);
    public abstract Item toDrop(int quantity, bool needRemove, Inventory inv);
    public bool isEmpty()
    {
        return this.quantity == 0;
    }
}

