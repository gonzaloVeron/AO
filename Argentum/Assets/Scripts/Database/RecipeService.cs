using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

public class RecipeService
{
    GenericMongoDAO<Recipe> mongodao;

    public RecipeService()
    {
        this.mongodao = new GenericMongoDAO<Recipe>(typeof(Recipe).ToString());
    }

    public void Save(Recipe r)
    {
        mongodao.Save(r);
    }

    public Recipe fetchRecipe(string name) => mongodao.get(this.query(name));

    public void UpdateRecipe(Recipe r)
    {
        mongodao.Update(this.query(r.name), r);
    }

    public void DeleteRecipe(string name)
    {
        mongodao.Delete(this.query(name));
    }

    public List<Recipe> recipesAvailable(IMongoQuery query)
    {
        List<Recipe> res = new List<Recipe>();
        foreach(Recipe r in this.mongodao.getMongoCollection().Find(query))
        {
            res.Add(r);
        }
        return res;
    }

    private IMongoQuery query(string st) => Query<Recipe>.EQ(doc => doc.name, st);
}
