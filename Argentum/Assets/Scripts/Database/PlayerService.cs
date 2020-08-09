using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

public class PlayerService
{
    private GenericMongoDAO<Player> mongodao;

    public PlayerService()
    {
        this.mongodao = new GenericMongoDAO<Player>(typeof(Player).ToString());
    }

    public void CreatePlayer(string name, Attributes attributes, Skills skills, Classification clasf)
    {
        Player newPlayer = new Player(name, attributes, skills, clasf);
        mongodao.Save(newPlayer);
    }

    //Deberia recibir el objeto ya modificado
    public void UpgradePlayer(Player ch)
    {
        mongodao.Update(this.query(ch.name), ch);
    }

    public void DeletePlayer(string name)
    {
        mongodao.Delete(this.query(name));
    }

    public Player fetchPlayer(string name) => mongodao.get(this.query(name));
    private IMongoQuery query(string st) => Query<Player>.EQ(doc => doc.name, st);

    public void DropCollection()
    {
        mongodao.DeleteAll();
    }
}