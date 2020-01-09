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
    public float weight;
    public Tuple<int, int> hitPoints;
    /*** Character State ***/
    /*** Character Equipment ***/
    public Armor armor;
    public Shield shield;
    public Weapon weapon;
    public Helmet helmet;
    public Inventory inv;
    public HashSet<Spell> spells;
    public LimitedList<Magical> magicalItemsEquiped; 
    /*** Character Equipment ***/
    
    public Character(string name, Attributes attributes, Skills skills, Classification clasf)
    {
        this.name = name;
        this.clasf = clasf;
        this.attributes = attributes;
        this.state = new State(this.initialLife(), this.clasf.initialMana(), 40, 100, 100); //Siempre se crea con esos params
        this.skills = skills;
        this.gold = 0;
        this.xp = 0;
        this.xpMax = 150;
        this.lvl = 1;
        this.armor = null;
        this.shield = null;
        this.weapon = null;
        this.helmet = null;
        this.magicalItemsEquiped = new LimitedList<Magical>(3);
        this.inv = new Inventory();
        this.spells = new HashSet<Spell>();
        this.hitPoints = new Tuple<int, int>(1, 2);
        this.weight = 0f;
    }
    public void Attack(Character other)
    {
        switch (this.weapon)
        {
            case Dagger dag:
                other.BeingAttacked(this.clasf.stabDamage(this.damage())); //Falta testear ! y calcular la probabilidad de apuñalar
                this.GainExperience(2);
                break;
            case Weapon weap:
                other.BeingAttacked(this.damage());
                this.GainExperience(2);
                break;
            default:
                throw new System.Exception("Algo salio mal en la funcion 'Attack'");
        }
        //Falta agregar la chance de acertar el golpe
    }
    public void castSpell(Spell s, Character other)
    {
        this.ControlMana(s.manaPointsNeeded);
        this.state.manaPoints -= Mathf.Max(0, s.manaPointsNeeded);
        s.Effect(this, other);
    }
    private void ControlMana(int manaPointsNeeded)
    {
        if (this.state.manaPoints < manaPointsNeeded)
        {
            throw new System.Exception("Mana insuficiente para lanzar el hechizo");
        }
    }
    public void BeingAttacked(int value)
    {
        this.state.lifePoints = Mathf.Max(0, this.state.lifePoints - Mathf.Max(0, (value - Random.Range(this.armor.minArmor() + this.shield.minShield() + this.helmet.minHelmet(), this.armor.maxArmor() + this.shield.maxShield() + this.helmet.maxHelmet() + 1))));
    }
    public void BeAttackedWithMagic(int value)
    {
        this.state.lifePoints -= value; //restar defensa magica a value y no restar daño magico menor a 0
        //Modificar Test
    }
    public void Heal(int value)
    {
        this.state.lifePoints = Mathf.Min(this.state.maxLifePoints, this.state.lifePoints + value);
    }
    public int damage() => Random.Range(this.physicalDamage(this.weapon.minWeapon(), this.hitPoints.item1), this.physicalDamage(this.weapon.maxWeapon(), this.hitPoints.item2) + 1);
    public int magicDamage(int minSpellDamage, int maxSpellDamage, int extraMagicDamage) => Mathf.RoundToInt((70 + extraMagicDamage) * ((float)this.spellDamage(minSpellDamage, maxSpellDamage) / 100));
    public int extraMagicDamage() => 0;
    public int spellDamage(int minDamage, int maxDamage) => Random.Range(Mathf.RoundToInt(minDamage + ((float)(minDamage * 3 * this.lvl) / 100)), Mathf.RoundToInt(maxDamage + ((float)(maxDamage * 3 * this.lvl) / 100)) + 1);
    private int physicalDamage(int damage, int hitPoints) => Mathf.RoundToInt(((damage * 3) + (((float)this.weapon.maxWeapon() / 5) * (this.attributes.strength - 15)) + hitPoints) * this.clasf.meleeDamageMod());
    public void ModifyState() { } //Implementar !!
    public void TakeItem(Item i)
    {
        switch (i)
        {
            case Consumable it when it.name == "Gold":
                this.gold += it.quantity;
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
        this.ControlDropGold(value);
        this.gold = Mathf.Max(0, this.gold - value);
        return new Consumable("Gold", 0, 0, 0, 0, 0, value, 0);
    }
    private void ControlDropGold(int value)
    {
        if(this.gold < value)
        {
            throw new System.Exception("No tienes oro suficiente");
        }
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
        this.inv.fetchItem(itemName).Use(this);;
    }
    public void EquipMagicalItem(Magical obj)
    {
        this.magicalItemsEquiped.Add(obj);
        this.weight += obj.weight;
    }
    public void UnequipMagicalItem(Magical obj)
    {
        this.magicalItemsEquiped.Remove(obj);
        this.weight -= obj.weight;
    }
    public Item dropItem(string name, int quantity) => this.inv.itemToDrop(name, quantity);
    private int initialLife() => 15 + (Mathf.RoundToInt(this.attributes.constitution / 3));
    public void EquipItem(Equipable obj)
    {
        switch (obj)
        {
            case Armor ar:
                this.armor = ar;
                break;
            case Helmet hel:
                this.helmet = hel;
                break;
            case Shield sh:
                this.shield = sh;
                break;
            case Weapon we:
                this.weapon = we;
                break;
            default:
                throw new System.Exception("No se puede equipar este item");
        }
        this.weight += obj.weight;
    }
    public void UnequipItem(Equipable obj)
    {
        switch (obj)
        {
            case Armor ar:
                this.armor = null;
                break;
            case Helmet hel:
                this.helmet = null;
                break;
            case Shield sh:
                this.shield = null;
                break;
            case Weapon we:
                this.weapon = null;
                break;
            default:
                throw new System.Exception("No se puede desequipar este item");
        }
        this.weight += obj.weight;
    }
    public bool isEquiped(Equipable obj)
    {
        switch (obj)
        {
            case Helmet hel:
                return this.helmet != null && this.helmet.name == obj.name;
            case Armor arm:
                return this.armor != null && this.helmet.name == obj.name;
            case Shield shi:
                return this.shield != null && this.shield.name == obj.name;
            case Weapon wea:
                return this.weapon != null && this.weapon.name == obj.name;
            case Magical mag:
                return this.magicalItemsEquiped.exists(s => s.name == mag.name);
            default:
                throw new System.Exception("Pasaron cosas en la funcion 'isEquiped' ");
        }
    }

}

