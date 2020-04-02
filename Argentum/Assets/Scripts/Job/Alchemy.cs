using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver.Builders;

public class Alchemy : Job
{
    public PotionService potionService;

    public Alchemy(Player player)
    {
        this.player = player;
        this.potionService = new PotionService();
        this.recipeService = new RecipeService();
    }
    public override void CraftItem(string itemName)
    {
        base.CraftItem(itemName);
        this.player.TakeItem(potionService.fetchPotion(itemName));
    }
    public override List<string> recipesAvailable(int alchemySkill) => recipeService.recipesAvailable(Query.And(Query<Recipe>.LTE(doc => doc.minimumSkillNecesary, alchemySkill), Query<Recipe>.EQ(doc => doc.type, "Alchemy"))).ConvertAll(r => r.name);
}

