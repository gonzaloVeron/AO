using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;

public class MongoConnection
{
    private static MongoConnection INSTANCE;

    private MongoClient client;

    private MongoDatabase db;

    public static MongoConnection getInstance()
    {
        if(INSTANCE == null)
        {
            INSTANCE = new MongoConnection();
        }
        return (INSTANCE);
    }

    public MongoConnection()
    {
        this.client = new MongoClient("mongodb://localhost:27017/AO");
        this.db = this.client.GetServer().GetDatabase("AO");
    }

    public MongoDatabase getDB() => this.db;
}
