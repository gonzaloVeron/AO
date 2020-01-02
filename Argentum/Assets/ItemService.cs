using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

public class ItemService
{
    GenericMongoDAO<Item> mongodao;

    public ItemService()
    {
        this.mongodao = new GenericMongoDAO<Item>(typeof(Item).ToString());
    }
    
    public void CreateItemConsumable(string name, int life, int mana, int ener, int hun, int thir, int gold)
    {
        Item item = new Consumable(name, life, mana, ener, hun, thir, gold, 0);
        mongodao.Save(item);
    }

    public void CreateItemEquipable(string name, int minArmor, int maxArmor, int minHelmet, int maxHelmet, int minShield, int maxShield, int minWeapon, int maxWeapon)
    {
        Item item = new Equipable(name, minArmor, maxArmor, minHelmet, maxHelmet, minShield, maxShield, minWeapon, maxWeapon, 0);
        mongodao.Save(item);
    }

    public Item fetchItem(string name)
    {
        return mongodao.get(this.query(name));
    }

    public void UpdateItem(Item i)
    {
        mongodao.Update(this.query(i.name), i);
    }

    public void DeleteItem(string name)
    {
        mongodao.Delete(this.query(name));
    }

    private IMongoQuery query(string st)
    {
        return Query<Item>.EQ(doc => doc.name, st);
    }

}
