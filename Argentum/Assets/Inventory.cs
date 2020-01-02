using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> inv;

    public Inventory()
    {
        this.inv = new List<Item>();
    }

    public void AddItem(Item i)
    {
        this.inv.Add(i);
    }

    public Item fetchItem(string name)
    {
        return inv.Find(i => i.name == name);
    }


    public void RemoveItem(Item i)
    {
        this.inv.Remove(i);
    }

    public bool existsItem(string name)
    {
        return this.inv.Exists(i => i.name == name);
    }

    public void AddQuantity(string name, int value)
    {
        this.fetchItem(name).quantity += value;
    }

    public bool isEmpty()
    {
        return inv.Count == 0;
    }

    //Funcion creada por culpa de fidel
    public void ApplyFun(System.Action<Item> algo, string name)
    {
        algo.Invoke(this.fetchItem(name));
    }

}
