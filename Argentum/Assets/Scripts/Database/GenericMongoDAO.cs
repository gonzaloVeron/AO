using System.Collections;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using UnityEngine;

public class GenericMongoDAO<T>
{
    protected MongoCollection<T> mongoCollection;

    public GenericMongoDAO(string typeString)
    {
        this.mongoCollection = MongoConnection.getInstance().getDB().GetCollection<T>(typeString);
        if(this.mongoCollection == null)
        {
            MongoConnection.CreateCollection(typeString);
        }
    }
    
    public void DeleteAll()
    {
        this.mongoCollection.Drop();
    }

    public T get(IMongoQuery query) => mongoCollection.FindOne(query);
    
    public void Save(T obj)
    {
        this.mongoCollection.Insert(obj);
    }

    public void Delete(IMongoQuery query)
    {
        this.mongoCollection.Remove(query, RemoveFlags.Single);
    }

    public void Update(IMongoQuery query, T obj)
    {
        this.mongoCollection.Update(query, Update<T>.Replace(obj));
    }

    public long size() => this.mongoCollection.Count();

    public MongoCollection<T> getMongoCollection() => this.mongoCollection;
}
