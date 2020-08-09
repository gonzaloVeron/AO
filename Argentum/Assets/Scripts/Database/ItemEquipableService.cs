using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
public class ItemEquipableService
{
    GenericMongoDAO<Equipable> mongodao;

    public ItemEquipableService()
    {
        this.mongodao = new GenericMongoDAO<Equipable>(typeof(Equipable).ToString());
    }
    public void Save(Equipable r)
    {
        mongodao.Save(r);
    }
    public Equipable fetchEquipable(string name) => mongodao.get(this.query(name));

    public void UpdateEquipable(Equipable e)
    {
        mongodao.Update(this.query(e.name), e);
    }
    public void DeleteResource(string name)
    {
        mongodao.Delete(this.query(name));
    }
    private IMongoQuery query(string st) => Query<Equipable>.EQ(doc => doc.name, st);

    public void DropCollection()
    {
        mongodao.DeleteAll();
    }
}
