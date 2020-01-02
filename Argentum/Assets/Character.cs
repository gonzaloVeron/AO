using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;

public class Character
{
    public ObjectId _id;
    /*** Character Identification ***/
    public string name;
    public Gender gender;
    public Classification clasf; //Representa la clase, ej: Paladin, Nigromante
    public Faction faction;
    /*** Character Identification ***/
    /*** Character State ***/
    public int gold;
    public int xp; //Experience
    public int xpMax;
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
    public HashSet<Spell> spells;
    /*** Character Equipment ***/

    public Character(string name, Attributes attributes, Skills skills)
    {
        this.name = name;
        this.gold = 0;
        this.xp = 0;
        this.xpMax = 10;
        this.lvl = 1;
        this.state = new State(100, 0, 100, 100); //hambre y sed siempre empieza en 100 y no se modifica nunca, la vida y la mana deberian calcularse con los atributos
        this.attributes = attributes;
        this.skills = skills;
        this.armor = null;
        this.shield = null;
        this.weapon = null;
        this.helmet = null;
        this.inventory = new List<Item>();
        this.spells = new HashSet<Spell>();
    }

    public void Attack(Character other)
    {
        //Falta agregar la chance de acertar el golpe
        other.BeingAttacked(this.damage());
        this.GainExperience(2);
    }

    //Lanzar hechizo
    public void castSpell(Character other, Spell s)
    {
        other.BeingAttacked(5); //Reemplazar 5 por s.magicDamage()    
    }

    public void BeingAttacked(int value)
    {
        this.state.lifePoints -= value;
        //Falta calcular la defensa total y restarla al daño "value"
    }

    public int damage()
    {
        return this.attributes.strength; //Falta agregar el daño del arma y otros
    }

    public void TakeItem(Item i)
    {
        this.inventory.Add(i);
    }

    public void LearnSpell(Spell s)
    {
        this.spells.Add(s);
    }

    public void GainExperience(int value)
    {
        if (this.needLevelUp(value))
        {
            this.LevelUp();
        }
        else
        {
            this.xp += value;
        }
    }

    public bool needLevelUp(int value)
    {
        return xp + value >= xpMax;
    }

    public void LevelUp()
    {
        this.lvl += 1;
        this.xp = 0;
        //falta aumentar la vida, la mana y la energia maxima
    }

    public void BuyItem(int value, Item i)
    {
        this.gold -= value;
        this.TakeItem(i);
    }

    public void SellItem(int value, Item i)
    {
        this.inventory.Remove(i);
        this.gold += value;
    }

    public void ChangeFaction(Faction faction)
    {
        this.faction = faction;
    }


}
