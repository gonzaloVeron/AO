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
    public void BeingTamed()
    {
        //Que deberia hacer ?
    }

}
