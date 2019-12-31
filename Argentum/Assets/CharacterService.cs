using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

public class CharacterService
{
    private GenericMongoDAO<Character> mongodao;

    public CharacterService()
    {
        this.mongodao = new GenericMongoDAO<Character>(typeof(Character).ToString());
    }

    //Method out of service
    public void CreateCharacter(string name)
    {
        //Character newCharacter = new Character(name);
        //mongodao.Save(newCharacter);
    }

    //Deberia recibir el objeto ya modificado
    public void UpgradeCharacter(Character ch)
    {
        mongodao.Update(this.query(ch.name), ch);
    }

    public void DeleteCharacter(string name)
    {
        mongodao.Delete(this.query(name));
    }

    public Character findCharacter(string name)
    {
        return mongodao.get(this.query(name));
    }

    private IMongoQuery query(string st)
    {
        return Query<Character>.EQ(doc => doc.name, st);
    }
    
}