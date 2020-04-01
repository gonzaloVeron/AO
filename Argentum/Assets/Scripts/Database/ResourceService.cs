using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

public class ResourceService
{
    GenericMongoDAO<Resource> mongodao;

    public ResourceService()
    {
        this.mongodao = new GenericMongoDAO<Resource>(typeof(Resource).ToString());
    }
    public void Save(Resource r)
    {
        mongodao.Save(r);
    }
    public Resource fetchResource(string name) => mongodao.get(this.query(name));

    public void UpdateResource(Resource r)
    {
        mongodao.Update(this.query(r.name), r);
    }
    public void DeleteResource(string name)
    {
        mongodao.Delete(this.query(name));
    }
    private IMongoQuery query(string st) => Query<Resource>.EQ(doc => doc.name, st);
}
