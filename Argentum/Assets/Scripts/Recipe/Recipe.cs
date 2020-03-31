using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(typeof(PotionRecipe))]
public class Recipe
{
    public string name;
    public int minimumSkillNecesary;

    public Recipe(string name, int minimumSkillNecesary)
    {
        this.minimumSkillNecesary = minimumSkillNecesary;
        this.name = name;
    }
}
