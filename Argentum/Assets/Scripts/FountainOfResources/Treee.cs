using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treee : FountainOfResources
{
    public Treee(int resourcesAmount)
    {
        this.resourceAmount = resourcesAmount;
    }
    public override string name() => "Tree";
    public override bool isValidTool(string toolName) => toolName == "Axe";
    public override void howToSubstract(Player player)
    {
        player.Cutdown(this);
    }


}
