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
    public override Item extractWithAxe(int value, int amount) => new Resource("Madera", amount, 0f);
    public override void HowToSubstract(Player player)
    {
        player.Cutdown(this);
    }
}
