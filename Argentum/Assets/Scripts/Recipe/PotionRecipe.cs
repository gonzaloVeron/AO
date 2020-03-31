using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionRecipe : Recipe
{
    public List<Tuple<string, int>> itemsNeeded;

    public PotionRecipe(string name, int minimumSkillNecesary) : base(name, minimumSkillNecesary)
    {
        this.itemsNeeded = new List<Tuple<string, int>>();
    }

    public void AddElement(string name, int amount)
    {
        this.itemsNeeded.Add(new Tuple<string, int>(name, amount));
    }
    public void RemoveElement(string name)
    {
        this.itemsNeeded = this.itemsNeeded.FindAll(i => i.item1 != name);
    }
}