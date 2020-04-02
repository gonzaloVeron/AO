using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;

public class Smithy : Job
{
    public ItemEquipableService ieService;

    public Smithy(Player player)
    {
        this.player = player;
        this.recipeService = new RecipeService();
        this.ieService = new ItemEquipableService();
    }
    public override void CraftItem(string itemName)
    {
        base.CraftItem(itemName);
        this.player.TakeItem(this.ieService.fetchEquipable(itemName));
    }
    public override List<string> recipesAvailable(int smithySkill) => recipeService.recipesAvailable(Query.And(Query<Recipe>.LTE(doc => doc.minimumSkillNecesary, smithySkill), Query<Recipe>.EQ(doc => doc.type, "Smithy"))).ConvertAll(r => r.name);
}
