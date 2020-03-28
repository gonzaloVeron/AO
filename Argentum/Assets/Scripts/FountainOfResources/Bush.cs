using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : FountainOfResources
{
    public Bush(int resourcesAmount)
    {
        this.resourceAmount = resourcesAmount;
    }
    public override string name() => "Bush";
    public override Item extractWithScissors(int value, int amount) => new Resource("Raiz", amount, 0f);
    public override void HowToSubstract(Player player)
    {
        player.ExtractRoots(this);
    }
}
