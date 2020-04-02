using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;

public class Recipe
{
    public ObjectId _id;
    public string name;
    public int minimumSkillNecesary;
    public string type;
    public List<Tuple<string, int>> itemsNeeded;

    public Recipe(string name, int minimumSkillNecesary, string type)
    {
        this.minimumSkillNecesary = minimumSkillNecesary;
        this.name = name;
        this.type = type;
        this.itemsNeeded = new List<Tuple<string, int>>();
    }
    public void AddElement(string name, int amount)
    {
        this.itemsNeeded.Add(new Tuple<string, int>(name, amount));
    }
    public void RemoveElement(string name)
    {
        this.itemsNeeded = this.itemsNeeded.FindAll(i => i.item1 != name);
    }
}
