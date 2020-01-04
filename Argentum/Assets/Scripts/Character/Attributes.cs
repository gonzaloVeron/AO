using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes 
{
    public int strength;
    public int agility;
    public int constitution;
    public int intelligence;
    public int charisma;
    public int lucky;
    public int wisdom; //Sabiduria

    public Attributes(int strength, int agility, int constitution, int intelligence, int charisma, int lucky, int wisdom)
    {
        this.strength = strength;
        this.agility = agility;
        this.constitution = constitution;
        this.intelligence = intelligence;
        this.charisma = charisma;
        this.lucky = lucky;
        this.wisdom = wisdom;
    }

}