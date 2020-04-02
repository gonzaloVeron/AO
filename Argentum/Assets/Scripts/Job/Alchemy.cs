using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;

public class Alchemy
{
    public Player player;

    public RecipeService recipeService;

    public PotionService potionService;

    public Alchemy(Player player)
    {
        this.player = player;
        this.potionService = new PotionService();
        this.recipeService = new RecipeService();
    }
    public void GeneratePotion(string potionName)
    {
        this.RemoveItemsFromRecipe(this.player, this.findItemsNeeded(potionName));
        this.player.TakeItem(potionService.fetchPotion(potionName));
    }
    public List<string> recipesAvailable(int alchemySkill) => recipeService.recipesAvailable(Query.And(Query<Recipe>.LTE(doc => doc.minimumSkillNecesary, alchemySkill), Query<Recipe>.EQ(doc => doc.type, "Alchemy"))).ConvertAll(r => r.name);
    public void RemoveItemsFromRecipe(Player player, List<Tuple<string, int>> itemsFromRecipe)
    {
        itemsFromRecipe.ForEach(i => player.inv.RemoveItemByQuantity(i.item1, i.item2));
    }
    public List<Tuple<string, int>> findItemsNeeded(string potionName) => this.recipeService.fetchRecipe(potionName).itemsNeeded;
}

