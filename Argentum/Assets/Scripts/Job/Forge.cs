using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

public class Forge : Job
{
    public ResourceService resourceService;

    public Forge(Player player)
    {
        this.player = player;
        this.recipeService = new RecipeService();
        this.resourceService = new ResourceService();
    }
    public override void CraftItem(string itemName) 
    {
        base.CraftItem(itemName);
        this.player.TakeItem(resourceService.fetchResource(itemName));
    }
    public override List<string> recipesAvailable(int mininSkill) => recipeService.recipesAvailable(Query.And(Query<Recipe>.LTE(doc => doc.minimumSkillNecesary, mininSkill), Query<Recipe>.EQ(doc => doc.type, "Forge"))).ConvertAll(r => r.name);
}
