using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Job
{
    protected Player player;

    protected RecipeService recipeService;

    public virtual void CraftItem(string itemName)
    {
        this.RemoveItemsFromRecipe(this.player, this.findItemsNeeded(itemName));
    }
    public abstract List<string> recipesAvailable(int skill);
    public void RemoveItemsFromRecipe(Player player, List<Tuple<string, int>> itemsFromRecipe)
    {
        itemsFromRecipe.ForEach(i => player.inv.RemoveItemByQuantity(i.item1, i.item2));
    }
    public List<Tuple<string, int>> findItemsNeeded(string potionName) => this.recipeService.fetchRecipe(potionName).itemsNeeded;
}
