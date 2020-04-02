using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

public class Forge
{
    public Player player;

    public RecipeService recipeService;

    public ResourceService resourceService;

    public Forge(Player player)
    {
        this.player = player;
        this.recipeService = new RecipeService();
        this.resourceService = new ResourceService();
    }
    public void GenerateIngot(string ingotName) 
    {
        var itemsNeeded = recipeService.fetchRecipe(ingotName).itemsNeeded[0];
        this.player.inv.RemoveItemByQuantity(itemsNeeded.item1 , itemsNeeded.item2);
        this.player.TakeItem(resourceService.fetchResource(ingotName));
    }
    public List<string> recipesAvailable(int mininSkill) => recipeService.recipesAvailable(Query<Recipe>.LTE(doc => doc.minimumSkillNecesary, mininSkill)).ConvertAll(r => r.name);
}
