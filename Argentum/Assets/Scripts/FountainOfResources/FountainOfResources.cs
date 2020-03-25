using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;

[BsonKnownTypes(typeof(Shoal), typeof(Treee))]
public abstract class FountainOfResources
{
    public int resourceAmount;

    public int resources(int value) => Mathf.Min(value, resourceAmount);

    public void substractResources(int value)
    {
        this.resourceAmount = Mathf.Max(0, this.resourceAmount - value);
    }

    public void verifyTool(string toolName)
    {
        if (!this.isValidTool(toolName))
        {
            throw new System.Exception(toolName + " no es la herramienta adecuada para el trabajo.");
        }
    }
    public abstract bool isValidTool(string toolName);
    public abstract void howToSubstract(Player player);
    public abstract string name();

}