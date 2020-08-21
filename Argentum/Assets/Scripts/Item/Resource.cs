using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Item
{
    public Resource(string name, int quantity, float weight)
    {
        this.name = name;
        this.quantity = quantity;
        this.weight = weight;
    }

    public override Item copy() => new Resource(this.name, this.quantity, this.weight);

    public override Item toDrop(int quantity, bool needRemove, Inventory inv)
    {
        if (needRemove)
        {
            inv.RemoveItem(this);
            return this.copy();
        }
        else
        {
            this.quantity -= quantity;
            return new Resource(this.name, quantity, this.weight);
        }
    }
    
    public override void Use(Player other)
    {
        //No tiene que hacer nada si se usa
    }

    public override void AddToInv(Inventory inv)
    {
        inv.AddItem(this);
    }
}
