using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> inv;

    public int copperCoins;

    public int silverCoins;

    public int goldCoins;

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

    public Item itemToDrop(string name, int quantity)
    {
        Item item = this.inv.Find(i => i.name == name);
        return item.toDrop(quantity, item.quantity - quantity <= 0,  this);
    }
    public void RemoveItemByQuantity(string name, int quantity)
    {
        Item item = this.fetchItem(name);
        if(item.quantity - quantity <= 0)
        {
            this.RemoveItem(item);
        }
        else
        {
            item.quantity -= quantity;
        }
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

    public int itemsAmount()
    {
        return this.inv.Count;
    }

    public Item getRandomItem()
    {
        var rand = Random.Range(0, this.inv.Count - 1);
        return this.inv[rand];
    }

    public GoldCoin removeGoldCoins(int value)
    {
        this.ControlDropCoins(value, this.goldCoins, "monedas de oro");
        this.goldCoins = Mathf.Max(0, this.goldCoins - value);
        return new GoldCoin(value);
    }

    public SilverCoin removeSilverCoins(int value)
    {
        this.ControlDropCoins(value, this.silverCoins, "monedas de plata");
        this.silverCoins = Mathf.Max(0, this.silverCoins - value);
        return new SilverCoin(value);
    }

    public CopperCoin removeCopperCoins(int value)
    {
        this.ControlDropCoins(value, this.copperCoins, "monedas de cobre");
        this.copperCoins = Mathf.Max(0, this.copperCoins - value);
        return new CopperCoin(value);
    }

    private void ControlDropCoins(int value, int coins, string coinType)
    {
        if(coins < value)
        {
            throw new NotEnoughCoins($"No tienes suficientes {coinType} !");
        }
    }

    public void TakeItem(Item i)
    {
        i.BeTaked(this);
    }

}
