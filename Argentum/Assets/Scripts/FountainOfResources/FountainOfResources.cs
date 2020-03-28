using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(typeof(Shoal), typeof(Treee), typeof(Bush), typeof(OreDeposit))]
public abstract class FountainOfResources
{
    public int resourceAmount;

    public int resources(int value) => Mathf.Min(value, resourceAmount);
    public void substractResources(int value)
    {
        this.resourceAmount = Mathf.Max(0, this.resourceAmount - value);
    }
    public abstract string name();
    public abstract void HowToSubstract(Player player);
    /*--------------------------------------------------*/
    public virtual Item extractWithAxe(int value, int amount)
    {
        throw new System.Exception("No es la herramienta adecuada para el trabajo.");
    }
    public virtual Item extractWithPickaxe(int value, int amount)
    {
        throw new System.Exception("No es la herramienta adecuada para el trabajo.");
    }
    public virtual Item extractWithScissors(int value, int amount)
    {
        throw new System.Exception("No es la herramienta adecuada para el trabajo.");
    }
    public virtual Item extractWithFishingRod(int value, int amount)
    {
        throw new System.Exception("No es la herramienta adecuada para el trabajo.");
    }
    public virtual Item extractWithFishingNet(int value, int amount)
    {
        throw new System.Exception("No es la herramienta adecuada para el trabajo.");
    }

}