using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

public class PotionService
{
    GenericMongoDAO<Potion> mongodao;

    public PotionService()
    {
        this.mongodao = new GenericMongoDAO<Potion>(typeof(Potion).ToString());
    }

    public void Save(Potion i)
    {
        mongodao.Save(i);
    }

    public Potion fetchPotion(string name) => mongodao.get(this.query(name));

    public void UpdatePotion(Potion p)
    {
        mongodao.Update(this.query(p.name), p);
    }

    public void DeletePotion(string name)
    {
        mongodao.Delete(this.query(name));
    }
    private IMongoQuery query(string st) => Query<Potion>.EQ(doc => doc.name, st);
}
