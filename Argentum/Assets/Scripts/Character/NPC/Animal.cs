using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Creature
{
    public float tameChance;
    public Animal(string name, int lifePoints, Tuple<int, int> hitPoints, int creatureDefense, int creatureAim, int creatureEvasion, int exp, List<Item> drop, float tameChance) : base(name, lifePoints, hitPoints, creatureDefense, creatureAim, creatureEvasion, exp, drop)
    {
        this.tameChance = tameChance;
    }
    public override void BeingTamed(Player player)
    {
        if (player.attributes.charisma * player.skills.tameAnimals * player.clasf.tameAnimalMod() >= this.tameChance)
        {
            player.tamedAnimals.Add(this);
            //ahora deberia desaparecer el gameobject del animal
        }
        else
        {
            throw new TheAnimalWasNotTamedException(this.name);
        }
    }

}
