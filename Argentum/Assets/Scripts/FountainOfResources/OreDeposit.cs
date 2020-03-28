using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;
[BsonKnownTypes(typeof(IronOreDeposit), typeof(SilverOreDeposit), typeof(GoldOreDeposit))]
public abstract class OreDeposit : FountainOfResources
{
    public override void HowToSubstract(Player player)
    {
        player.Mine(this);
    }
}

public class IronOreDeposit : OreDeposit
{
    public IronOreDeposit(int resourceAmount)
    {
        this.resourceAmount = resourceAmount;
    }
    public override Item extractWithPickaxe(int value, int amount) => new Resource("Mineral de hierro", amount, 0f);
    public override string name() => "IronOreDeposit";
}
public class SilverOreDeposit : OreDeposit
{
    public SilverOreDeposit(int resourceAmount)
    {
        this.resourceAmount = resourceAmount;
    }
    public override Item extractWithPickaxe(int value, int amount) => new Resource("Mineral de plata", amount, 0f);
    public override string name() => "SilverOreDeposit";
}
public class GoldOreDeposit : OreDeposit
{
    public GoldOreDeposit(int resourceAmount)
    {
        this.resourceAmount = resourceAmount;
    }
    public override Item extractWithPickaxe(int value, int amount) => new Resource("Mineral de oro", amount, 0f);
    public override string name() => "GoldOreDeposit";
}
