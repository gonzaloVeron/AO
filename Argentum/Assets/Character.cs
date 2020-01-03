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
    public Tuple<int, int> armor;
    public Tuple<int, int> shield;
    public Tuple<int, int> weapon;
    public Tuple<int, int> helmet;
    public Inventory inv;
    public HashSet<Spell> spells;
    /*** Character Equipment ***/

    public Character(string name, Attributes attributes, Skills skills)
    {
        this.name = name;
        this.gold = 0;
        this.xp = 0;
        this.xpMax = 10;
        this.lvl = 1;
        this.state = new State(100, 0, 0, 100, 100); //hambre y sed siempre empieza en 100 y no se modifica nunca, la vida y la mana deberian calcularse con los atributos
        this.attributes = attributes;
        this.skills = skills;
        this.armor = new Tuple<int, int>(0, 0);
        this.shield = new Tuple<int, int>(0, 0);
        this.weapon = new Tuple<int, int>(0, 0);
        this.helmet = new Tuple<int, int>(0, 0);
        this.inv = new Inventory();
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
        switch (i)
        {
            case Consumable it when it.name == "Gold":
                this.gold += it.gold;
                break;
            case Item it when this.inv.existsItem(it.name):
                this.inv.AddQuantity(it.name, it.quantity);
                break;
            default:
                this.inv.AddItem(i);
                break;
        }
    }

    public Consumable dropGold(int value)
    {
        this.gold -= value;
        return new Consumable("Gold", 0, 0, 0, 0, 0, value, 1);
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
        this.inv.RemoveItemByQuantity(i.name, i.quantity);
        this.gold += value;
    }

    public void ChangeFaction(Faction faction)
    {
        this.faction = faction;
    }

    public void UseItem(string itemName)
    {
        var item = this.inv.fetchItem(itemName);
        item.Use(this);
        if (item.isEmpty())
        {
            this.inv.RemoveItem(item);
        }
    }

    public Item dropItem(string name, int quantity)
    {
        return this.inv.itemToDrop(name, quantity);
    }
}
