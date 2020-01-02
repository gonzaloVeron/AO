﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;

public abstract class Item
{
    public ObjectId _id;

    public string name;

    public int quantity;

    public abstract void Use(Character other);
    public bool isEmpty()
    {
        return this.quantity == 0;
    }
}

