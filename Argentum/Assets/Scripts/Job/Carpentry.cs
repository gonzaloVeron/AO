using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;

public class Carpentry : Job
{
    public ItemEquipableService ieService;

    public Carpentry(Player player)
    {
        this.player = player;
        this.recipeService = new RecipeService();
        this.ieService = new ItemEquipableService();
    }
    public override void CraftItem(string itemName)
    {
        base.CraftItem(itemName);
        this.player.TakeItem(ieService.fetchEquipable(itemName));
    }
    public override List<string> recipesAvailable(int carpentrySkill) => recipeService.recipesAvailable(Query.And(Query<Recipe>.LTE(doc => doc.minimumSkillNecesary, carpentrySkill), Query<Recipe>.EQ(doc => doc.type, "Carpentry"))).ConvertAll(r => r.name);
}
