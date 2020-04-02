using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;

public class Smithy
{
    public Player player;

    public RecipeService recipeService;

    public ItemEquipableService ieService;

    public Smithy(Player player)
    {
        this.player = player;
        this.recipeService = new RecipeService();
        this.ieService = new ItemEquipableService();
    }

    public void CraftItem(string itemName)
    {
        this.RemoveItemsFromRecipe(this.player, this.findItemsNeeded(itemName));
        this.player.TakeItem(ieService.fetchEquipable(itemName));
    }
    public List<string> recipesAvailable(int smithySkill) => recipeService.recipesAvailable(Query<Recipe>.LTE(doc => doc.minimumSkillNecesary, smithySkill)).ConvertAll(r => r.name);
    public void RemoveItemsFromRecipe(Player player, List<Tuple<string, int>> itemsFromRecipe)
    {
        itemsFromRecipe.ForEach(i => player.inv.RemoveItemByQuantity(i.item1, i.item2));
    }
    public List<Tuple<string, int>> findItemsNeeded(string itemName) => this.recipeService.fetchRecipe(itemName).itemsNeeded;

}
