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
    public Tuple<int, int> hitPoints;

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
        this.hitPoints = new Tuple<int, int>(1, 2);
    }

    public void Attack(Character other)
    {
        //Falta agregar la chance de acertar el golpe
        other.BeingAttacked(this.damage());
        this.GainExperience(2);
    }

    //Lanzar hechizo
    public void castSpell(Spell s, Character other)
    {
        if (this.state.manaPoints < s.manaPointsNeeded) {
            throw new System.Exception("Mana insuficiente");
        }
        this.state.manaPoints -= Mathf.Max(0, s.manaPointsNeeded);
        switch (s)
        {
            case DirectDamage spell:
                other.BeAttackedWithMagic(this.magicDamage(s.minDamage, s.maxDamage, this.extraMagicDamage())); 
                break;
            case Healing spell:
                other.state.lifePoints += this.magicDamage(s.minDamage, s.maxDamage, this.extraMagicDamage());
                break;
            case Invocation spell:
                //deberia devolver un objeto npc de tipo ayudante o mascota
                break;
            case ModState spell:
                //deberia cambiar el estado del afectado
                break;
            default:
                break;
        }
    }

    public void BeingAttacked(int value)
    {
        this.state.lifePoints -= Mathf.Max(0, value - Random.Range(this.armor.item1 + this.shield.item1 + this.helmet.item1, this.armor.item2 + this.shield.item2 + this.helmet.item2 + 1));
    }

    public void BeAttackedWithMagic(int value)
    {
        this.state.lifePoints -= value; //restar defensa magica a value y no restar daño magico menor a 0
    }

    public int damage()
    {
        return Random.Range(this.physicalDamage(this.weapon.item1, this.hitPoints.item1), this.physicalDamage(this.weapon.item2, this.hitPoints.item2));
    }

    public int magicDamage(int minSpellDamage, int maxSpellDamage, int extraMagicDamage)
    {
        return Mathf.RoundToInt((70 + extraMagicDamage) * ((float)this.spellDamage(minSpellDamage, maxSpellDamage) / 100));
    }
    public int extraMagicDamage()
    {
        return 0;
    }

    public int spellDamage(int minDamage, int maxDamage)
    {
        return Random.Range(Mathf.RoundToInt(minDamage + ((float)(minDamage * 3 * this.lvl) / 100)), Mathf.RoundToInt(maxDamage + ((float)(maxDamage * 3 * this.lvl) / 100)) + 1);
    }

    private int physicalDamage(int damage, int hitPoints)
    {
        return Mathf.RoundToInt(((damage * 3) + (((float)this.weapon.item2 / 5) * (this.attributes.strength - 15)) + hitPoints) * this.clasf.meleeDamageMod());
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
