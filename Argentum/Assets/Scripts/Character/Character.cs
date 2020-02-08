﻿using System.Collections;
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
    public SpellsBook spells;
    public LimitedList<Magical> magicalItemsEquiped;
    public Arrow arrow;
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
        this.spells = new SpellsBook();
        this.hitPoints = new Tuple<int, int>(1, 2);
        this.weight = 0f;
    }
    public void Attack(Character other)
    {
        if (other.hasAShield())
        {
            var probConEsc = this.probabilidadDeAcierto(this.weapon.requiredSkill(this.skills), this.attributes.agility, other);
            var probRand = Random.Range(0, 101);
            Debug.Log("Probabilidad con escudo: " + probConEsc);
            Debug.Log("Numero random: " + probRand);
            if (probRand <= probConEsc)
            {
                Debug.Log("No falle !");
                this.weapon.HowToAttack(this, other);
                this.GainExperience(2);
            }
            else
            {
                throw new FailedAttackException(this.name);
            }
        }
        else
        {
            var probSinEsc = this.probabilidadDeAciertoSinEscudo(this.weapon.requiredSkill(this.skills), this.attributes.agility, other);
            var probRand = Random.Range(0, 101);
            Debug.Log("Probabilidad sin escudo: " + probSinEsc);
            Debug.Log("Numero random: " + probRand);
            if (probRand <= probSinEsc)
            {
                Debug.Log("No falle !");
                this.weapon.HowToAttack(this, other);
                this.GainExperience(2);
            }
            else
            {
                throw new FailedAttackException(this.name);
            }
        }
    }
    //Skill del tipo de arma que usa, ej: armedCombat, martial arts, projectile weapons
    public int probabilidadDeAcierto(int skill, int agility, Character other) => Mathf.Max(10, Mathf.Min(90, 50 + Mathf.RoundToInt(0.4f * (((skill + agility * this.skillAmountModificator(skill)) * this.aimModificator(this.weapon) + 2.5f * Mathf.Max(this.lvl - 12, 0)) - (other.skills.shieldDefese * 0.5f * other.clasf.defenseShieldMod() + (other.skills.combatTactics + ((float)other.skills.combatTactics / 33) * other.attributes.agility) * other.clasf.defenseEvasionMod() + 2.5f * Mathf.Max(other.lvl - 12, 0))))));
    public int probabilidadDeAciertoSinEscudo(int skill, int agility, Character other) => Mathf.Max(10, Mathf.Min(90, 50 + Mathf.RoundToInt(0.4f * (((skill + agility * this.skillAmountModificator(skill)) * this.aimModificator(this.weapon) + 2.5f * Mathf.Max(this.lvl - 12, 0)) - ((other.skills.combatTactics + ((float)other.skills.combatTactics / 33) * other.attributes.agility) * other.clasf.defenseEvasionMod() + 2.5f * Mathf.Max(other.lvl - 12, 0))))));
    public bool hasAShield() => this.shield != null;
    public float aimModificator(Weapon w) => w.modForWeapon(this.clasf);
    public float damageModificator(Weapon w) => w.damageMod(this.clasf);
    public int skillAmountModificator(float skill)
    {
        switch (skill)
        {
            case float n when n < 31:
                return 0;
            case float n when n < 61:
                return 1;
            case float n when n < 91:
                return 2;
            default:
                return 3;
        }
    }
    public void castSpell(Spell s, Character other)
    {
        this.ControlMana(s.manaPointsNeeded);
        this.state.manaPoints = Mathf.Max(0, this.state.manaPoints - s.manaPointsNeeded);
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
        this.state.lifePoints = Mathf.Max(0, this.state.lifePoints - Mathf.Max(0, value - this.physicalDefense()));
    }
    public int physicalDefense() => Random.Range(this.minArmor() + this.minShield() + this.minHelmet(), this.maxArmor() + this.maxShield() + this.maxHelmet() + 1);

    //-----//
    public int minArmor() => this.armor != null ? this.armor.minArmor() : 0;
    public int maxArmor() => this.armor != null ? this.armor.maxArmor() : 0;
    public int minHelmet() => this.helmet != null ? this.helmet.minHelmet() : 0;
    public int maxHelmet() => this.helmet != null ? this.helmet.maxHelmet() : 0;
    public int minShield() => this.shield != null ? this.shield.minShield() : 0;
    public int maxShield() => this.shield != null ? this.shield.maxShield() : 0;
    //-----//
    
    public void BeAttackedWithMagic(int value)
    {
        this.state.lifePoints = Mathf.Max(0, this.state.lifePoints - Mathf.Max(0, value - this.magicDefense()));
    }
    public void Heal(int value)
    {
        this.state.lifePoints = Mathf.Min(this.state.maxLifePoints, this.state.lifePoints + value);
    }
    public int damage() => Random.Range(this.physicalDamage(this.weapon.minWeapon(), this.hitPoints.item1, this.damageModificator(this.weapon)), this.physicalDamage(this.weapon.maxWeapon() , this.hitPoints.item2, this.damageModificator(this.weapon)) + 1);
    public int damageWithBow() => Random.Range(this.physicalDamage((this.weapon.minWeapon() + this.minArrow()), this.hitPoints.item1, this.damageModificator(this.weapon)), this.physicalDamage((this.weapon.maxWeapon() + this.maxArrow()), this.hitPoints.item2, this.damageModificator(this.weapon)) + 1);
    public int magicDamage(int minSpellDamage, int maxSpellDamage, int extraMagicDamage) => Mathf.RoundToInt((70 + extraMagicDamage) * ((float)this.spellDamage(minSpellDamage, maxSpellDamage) / 100));
    public int extraMagicDamage() => (this.magicalItemsEquiped.size == 0) ? 0 : this.magicalItemsEquiped.sum(i => i.magicalDamage) + this.weapon.magicalDamage;
    public int magicDefense() => this.magicalItemsEquiped.sum(i => i.magicalDefense) + this.armor.magicalDefense + this.shield.magicalDefense + this.helmet.magicalDefense;
    public int spellDamage(int minDamage, int maxDamage) => Random.Range(Mathf.RoundToInt(minDamage + ((float)(minDamage * 3 * this.lvl) / 100)), Mathf.RoundToInt(maxDamage + ((float)(maxDamage * 3 * this.lvl) / 100)) + 1);
    public int physicalDamage(int damage, int hitPoints, float modificator) => Mathf.RoundToInt(((damage * 3) + (((float)this.weapon.maxWeapon() / 5) * (this.attributes.strength - 15)) + hitPoints) * modificator);
    public int minArrow() => (this.arrow != null) ? this.arrow.damage.item1 : 0;
    public int maxArrow() => (this.arrow != null) ? this.arrow.damage.item2 : 0;
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
        this.state.maxLifePoints += this.clasf.calculateLifePointsPerLevel(this.attributes.constitution);
        this.state.maxManaPoints += this.clasf.calculateManaPerLevel(this.attributes.intelligence);
        this.state.maxEnergyPoints += this.clasf.energyPerLevel();
        this.incrementHitPoints(this.clasf.hitPointsPerLevel(this.lvl));
        this.lvl += 1;
        this.xpMax = this.xp * 2; //Reemplazar esta asignacion por otra que traiga el siguiente lvl de la base de datos
        this.xp = 0;
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
            case Arrow arr:
                this.arrow = arr;
                break;
            case Magical mag:
                this.magicalItemsEquiped.Add(mag);
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
            case Arrow arr:
                this.arrow = null;
                break;
            case Magical mag:
                this.magicalItemsEquiped.Remove(mag);
                break;
            default:
                throw new System.Exception("No se puede desequipar este item");
        }
        this.weight -= obj.weight;
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
            case Arrow arr:
                return this.arrow != null && this.weapon.name == obj.name;
            default:
                throw new System.Exception("Pasaron cosas en la funcion 'isEquiped' ");
        }
    }
    public bool hasAmmunition() => this.arrow != null;
    public void DiscardAmmunition()
    {
        this.inv.RemoveItemByQuantity(this.arrow.name, 1);
        if(this.arrow.quantity - 1 <= 0)
        {
            this.arrow = null;
        }
    }
    public void incrementHitPoints(int n)
    {
        switch (this.clasf)
        {
            case Bandit cl:
                this.hitPoints.item1 += n;
                this.hitPoints.item2 += n;
                break;
            default:
                this.hitPoints.item1 = (this.lvl < 35) ? Mathf.Min(99, this.hitPoints.item1 + n) : this.hitPoints.item1 + n;
                this.hitPoints.item2 = (this.lvl < 35) ? Mathf.Min(99, this.hitPoints.item2 + n) : this.hitPoints.item2 + n;
                break;
        }
    }
    public void ModifyState() { }
}

