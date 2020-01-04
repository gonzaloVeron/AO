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

    public void CreateCharacter(string name, Attributes attributes, Skills skills)
    {
        Character newCharacter = new Character(name, attributes, skills);
        mongodao.Save(newCharacter);
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

    public Character fetchCharacter(string name)
    {
        return mongodao.get(this.query(name));
    }

    private IMongoQuery query(string st)
    {
        return Query<Character>.EQ(doc => doc.name, st);
    }
    
}