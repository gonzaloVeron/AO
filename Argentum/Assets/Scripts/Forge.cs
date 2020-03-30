using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge
{
    public Player player;

    public Forge(Player player)
    {
        this.player = player;
    }

    public void GenerateIngot(Resource re) /* Esta funcion funde 1 solo lingote, deberia hacerse un funcion que ponga esta funcion en loop */
    { /* ¿Quiza un objeto fragua que abra una ventana para seleccionar que mineral fundir y que lo ponga en loop? */
        switch (re)
        {
            case Resource r when r.name.Contains("hierro"):
                this.GITemplate("hierro", this.player.skills.mining, r.quantity, 7, 25);
                break;
            case Resource r when r.name.Contains("plata"):
                this.GITemplate("plata", this.player.skills.mining, r.quantity, 20, 50);
                break;
            case Resource r when r.name.Contains("oro"):
                this.GITemplate("oro", this.player.skills.mining, r.quantity, 35, 100);
                break;
            default:
                throw new InvalidResourceException(re.name);
        }
    }
    public void GITemplate(string mineral, int miningSkill, int actualResourceAmount, int minimumResourceAmount, int minimumSkillNeeded)
    {
        this.verifyResourceAmount(actualResourceAmount, minimumResourceAmount);
        this.verifyMiningSkillForFoundry(miningSkill, minimumSkillNeeded);
        this.player.inv.RemoveItemByQuantity("Mineral de " + mineral, minimumResourceAmount);
        this.player.TakeItem(new Resource("Lingote de " + mineral, 1, 0f));
    }

    public void verifyResourceAmount(int resourceAmount, int value)
    {
        if (resourceAmount < value)
        {
            throw new InsufficientResourcesException();
        }
    }
    public void verifyMiningSkillForFoundry(int skill, int value)
    {
        if (skill < value)
        {
            throw new DontHaveEnoughSkillsException("mineria");
        }
    }
}
