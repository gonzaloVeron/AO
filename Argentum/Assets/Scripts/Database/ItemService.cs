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
    
    public void CreateItemConsumable(string name, int life, int mana, int ener, int hun, int thir)
    {
        Item item = new Consumable(name, life, mana, ener, hun, thir, 0, 0);
        mongodao.Save(item);
    }

    // ARREGLAR !!!
    public void CreateItemEquipable(string name, float weight)
    {
        //Item item = new Equipable(name, 0, weight); 
        //mongodao.Save(item);
    }

    public Item fetchItem(string name) => mongodao.get(this.query(name));

    public void UpdateItem(Item i)
    {
        mongodao.Update(this.query(i.name), i);
    }

    public void DeleteItem(string name)
    {
        mongodao.Delete(this.query(name));
    }

    private IMongoQuery query(string st) => Query<Item>.EQ(doc => doc.name, st);

}
