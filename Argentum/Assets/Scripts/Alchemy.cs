using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alchemy
{
    public Player player;

    public List<PotionRecipe> potionRecipes;

    public List<Consumable> potions = new List<Consumable>() { new Consumable("Pocion roja", 30, 0, 0, 0, 0, 1, 0f), new Consumable("Pocion de regeneracion", 5, 0, 0, 0, 0, 1, 0f), new Consumable("Pocion azul", 0, 5, 0, 0, 0, 1, 0f) };
    public Alchemy(Player player)
    {
        this.player = player;
        this.potionRecipes = new List<PotionRecipe>();

        var recipe1 = new PotionRecipe("Pocion de regeneracion", 1);
        recipe1.AddElement("Raiz", 1);
        recipe1.AddElement("Raiz", 1);
        recipe1.AddElement("Raiz", 1);
        recipe1.AddElement("Raiz", 1);
        recipe1.AddElement("Raiz", 1);

        var recipe2 = new PotionRecipe("Pocion azul", 20);
        recipe2.AddElement("Raiz", 55);

        var recipe3 = new PotionRecipe("Pocion roja", 30);
        recipe3.AddElement("Raiz", 70);


        this.potionRecipes.Add(recipe1);
        this.potionRecipes.Add(recipe2);
        this.potionRecipes.Add(recipe3);
    }

    public void GeneratePotion(string potionName)
    {
        this.RemoveItemsFromRecipe(this.player, this.findItemsNeeded(potionName));
        var newPotion = potions.Find(p => p.name == potionName).copy(); //Esto hay que cambiarlo por un llamado a la db
        this.player.TakeItem(newPotion);
    }
    /* esto tambien hay que cambiarlo a query de db - ¿es posible?*/
    public List<string> recipesAvailable(int alchemySkill) => this.potionRecipes.ConvertAll(i => new Tuple<string, int>(i.name, i.minimumSkillNecesary)).FindAll(t => t.item2 <= alchemySkill).ConvertAll(t => t.item1);

    public void RemoveItemsFromRecipe(Player player, List<Tuple<string, int>> itemsFromRecipe)
    {
        itemsFromRecipe.ForEach(i => player.inv.RemoveItemByQuantity(i.item1, i.item2));
    }
    /* esto tambien hay que cambiarlo a query de db */
    public List<Tuple<string, int>> findItemsNeeded(string potionName) => this.potionRecipes.Find(i => i.name == potionName).itemsNeeded;
}

