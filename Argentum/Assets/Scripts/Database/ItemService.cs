using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class ItemService
{
    GenericMongoDAO<Item> mongodao;
     
    public ItemService()
    {
        this.mongodao = new GenericMongoDAO<Item>(typeof(Item).ToString());
    }
    
    public void Save(Item i)
    {
        mongodao.Save(i);
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
