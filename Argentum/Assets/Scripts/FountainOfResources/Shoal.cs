using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoal : FountainOfResources
{
    public Shoal(int resourceAmount)
    {
        this.resourceAmount = resourceAmount;
    }
    public override string name() => "Shoal";
    public override bool isValidTool(string toolName) => toolName == "Fishing rod" || toolName == "Fishing net";
    public override void howToSubstract(Player player)
    {
        player.CatchFish(this);
    }
}
