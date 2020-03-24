using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainOfResources : Character
{
    public int resourceAmount;
    public FountainOfResources(string name, int resourceAmount)
    {
        this.name = name;
        this.resourceAmount = resourceAmount;
    }
    public override void Attack(Character other)
    {
        throw new System.Exception("No puede atacar");
    }

    public override float evasion() => 0f;

    public override int physicalDefense() => 0;

    public override int successProbability(Character other) => 0;
}
