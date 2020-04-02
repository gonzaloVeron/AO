using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;

public class Tailor : Job
{
    public ItemEquipableService ieService;

    public Tailor(Player player)
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
    public override List<string> recipesAvailable(int tailoringSkill) => recipeService.recipesAvailable(Query.And(Query<Recipe>.LTE(doc => doc.minimumSkillNecesary, tailoringSkill), Query<Recipe>.EQ(doc => doc.type, "Tailor"))).ConvertAll(r => r.name);
}
