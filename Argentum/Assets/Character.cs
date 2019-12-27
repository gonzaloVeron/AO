using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;

public class Character
{

    Item oneItem;

    private const string MONGO_URI = "";

    private const string DATABASE_NAME = "";

    MongoClient client;

    MongoServer server;

    MongoDatabase db;

    public Character(Item item)
    {
        oneItem = item;
    }

    public void Init()
    {
        client = new MongoClient(MONGO_URI);
        server = client.GetServer();
        db = server.GetDatabase(DATABASE_NAME);
    }

    public void setItem(Item oneItem)
    {
        this.oneItem = oneItem;
    }

    public Item getItem()
    {
        return this.oneItem;
    }

}
