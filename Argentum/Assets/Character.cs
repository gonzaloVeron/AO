using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;

public class Character
{
    public ObjectId _id;
    /*** Character State ***/
    public string name;
    public int gold;
    public float xp; //Experience
    public int lvl; //Level
    public State state;
    public Attributes attributes;
    public Skills skills;
    /*** Character State ***/
    /*** Character Equipment ***/
    public Item armor;
    public Item shield;
    public Item weapon;
    public Item helmet;
    public List<Item> inventory;
    public List<Spell> spells;
    /*** Character Equipment ***/

    public Character(string name, Attributes attributes, Skills skills)
    {
        this.name = name;
        this.gold = 0;
        this.xp = 0f;
        this.lvl = 1;
        this.state = new State(100, 0, 100, 100); //hambre y sed siempre empieza en 100 y no se modifica nunca, la vida y la mana deberian calcularse con los atributos
        this.attributes = attributes;
        this.skills = skills;
        this.armor = null;
        this.shield = null;
        this.weapon = null;
        this.helmet = null;
        this.inventory = new List<Item>();
        this.spells = new List<Spell>();
    }





}
