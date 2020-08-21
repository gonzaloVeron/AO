using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Net.Mail;
using System.Net;
using UnityEngine;
using MongoDB.Bson;

public class Player : Character
{
    /*** Player Identification ***/
    public Gender gender;
    public Classification clasf; //Representa la clase, ej: Paladin, Nigromante
    public Faction faction;
    /*** Player Identification ***/

    /*** Player State ***/
    public int xp; //Experience
    public int xpMax;
    public int lvl; //Level
    public Attributes attributes;
    public Skills skills;
    public float weight;
    /*** Player State ***/

    /*** Player Equipment ***/
    public Armor armor;
    public Shield shield;
    public Weapon weapon;
    public Helmet helmet;
    public Inventory inv;
    public SpellsBook spells;
    public LimitedList<Magical> magicalItemsEquiped;
    public Arrow arrow;
    public Tool tool;
    /*** Player Equipment ***/
    public LimitedList<Animal> tamedAnimals;

    public Player(string name, Attributes attributes, Skills skills, Classification clasf)
    {
        this.name = name;
        this.clasf = clasf;
        this.attributes = attributes;
        this.state = new State(this.initialLife(), this.clasf.initialMana(), 40, 100, 100); //Siempre se crea con esos params
        this.skills = skills;
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
        this.tamedAnimals = new LimitedList<Animal>(3);
    }

    public void TameAnimal(Creature a)
    {
        a.BeingTamed(this);
    }

    public override void Attack(Character other)
    {
        var prob = this.successProbability(other);
        var probRand = Random.Range(0, 101);
        Debug.Log("Probabilidad de acierto: " + prob);
        Debug.Log("Numero random: " + probRand);
        if (probRand <= prob)
        {
            Debug.Log("No falle !");
            this.weapon.HowToAttack(this, other);
            this.GainExperience(2);  //Modificar esto
        }
        else
        {
            Debug.Log("Falle !");
            throw new FailedAttackException(this.name);
        }
    }
    //Skill del tipo de arma que usa, ej: armedCombat, martial arts, projectile weapons        el calculo contempla si usa escudo o no
    public override int successProbability(Character other) => Mathf.Max(10, Mathf.Min(90, 50 + Mathf.RoundToInt(0.4f * (this.aim() - other.evasion()))));
    public float aim() => ((this.weapon.requiredSkill(this.skills) + this.attributes.agility * this.skillAmountModificator(this.weapon.requiredSkill(this.skills))) * this.aimModificator(this.weapon) + 2.5f * Mathf.Max(this.lvl - 12, 0));
    public override float evasion() => this.shielding() + (this.skills.combatTactics + ((float)this.skills.combatTactics / 33) * this.attributes.agility) * this.clasf.defenseEvasionMod() + 2.5f * Mathf.Max(this.lvl - 12, 0);
    public float shielding() => (this.shield != null) ? this.skills.shieldDefese * 0.5f * this.clasf.defenseShieldMod() : 0f;
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
    public void castSpell(Spell s, Player other)
    {
        this.ControlMana(s.manaPointsNeeded);
        this.state.manaPoints = Mathf.Max(0, this.state.manaPoints - s.manaPointsNeeded);
        s.Effect(this, other);
    }
    private void ControlMana(int manaPointsNeeded)
    {
        if (this.state.manaPoints < manaPointsNeeded)
        {
            throw new NotEnoughMana("Mana insuficiente para lanzar el hechizo");
        }
    }
    public override void BeingAttacked(int value)
    {
        this.state.lifePoints = Mathf.Max(0, this.state.lifePoints - Mathf.Max(0, value - this.physicalDefense()));
    }
    public override int physicalDefense() => Random.Range(this.minArmor() + this.minShield() + this.minHelmet(), this.maxArmor() + this.maxShield() + this.maxHelmet() + 1);

    //-----//
    public int minArmor() => this.armor != null ? this.armor.minArmor() : 0;
    public int maxArmor() => this.armor != null ? this.armor.maxArmor() : 0;
    public int minHelmet() => this.helmet != null ? this.helmet.minHelmet() : 0;
    public int maxHelmet() => this.helmet != null ? this.helmet.maxHelmet() : 0;
    public int minShield() => this.shield != null ? this.shield.minShield() : 0;
    public int maxShield() => this.shield != null ? this.shield.maxShield() : 0;
    //-----//
    
    public void BeAttackedWithMagic(int value) // BeingAttacked == BeAttackedWithMagic ¿?
    {
        this.state.lifePoints = Mathf.Max(0, this.state.lifePoints - Mathf.Max(0, value - this.magicDefense()));
    }
    public void Heal(int value)
    {
        this.state.lifePoints = Mathf.Min(this.state.maxLifePoints, this.state.lifePoints + value);
    }
    public void HealMana(int value)
    {
        this.state.manaPoints = Mathf.Min(this.state.maxManaPoints, this.state.manaPoints + value);
    }
    public int damage() => Random.Range(this.physicalDamage(this.weapon.minWeapon(), this.hitPoints.item1, this.damageModificator(this.weapon)), this.physicalDamage(this.weapon.maxWeapon() , this.hitPoints.item2, this.damageModificator(this.weapon)) + 1);
    public int damageWithBow() => Random.Range(this.physicalDamage((this.weapon.minWeapon() + this.minArrow()), this.hitPoints.item1, this.damageModificator(this.weapon)), this.physicalDamage((this.weapon.maxWeapon() + this.maxArrow()), this.hitPoints.item2, this.damageModificator(this.weapon)) + 1);
    public int magicDamage(int minSpellDamage, int maxSpellDamage, int extraMagicDamage) => Mathf.RoundToInt((float)this.spellDamage(minSpellDamage, maxSpellDamage) * this.clasf.magicalDamageMod() + this.extraMagicDamage());
    public int extraMagicDamage() => ((this.magicalItemsEquiped.size == 0) ? 0 : this.magicalItemsEquiped.sum(i => i.magicalDamage)) + ((this.weapon == null) ? 0 : this.weapon.magicalDamage);
    public int magicDefense() => this.magicalItemsEquiped.sum(i => i.magicalDefense) + this.armor.magicalDefense + this.shield.magicalDefense + this.helmet.magicalDefense + Mathf.RoundToInt(this.skills.magicResist * 0.1f);
    public int spellDamage(int minDamage, int maxDamage) => Random.Range(Mathf.RoundToInt(minDamage + ((float)(minDamage * 3 * this.lvl) / 100)), Mathf.RoundToInt(maxDamage + ((float)(maxDamage * 3 * this.lvl) / 100)) + 1);
    public int physicalDamage(int damage, int hitPoints, float modificator) => Mathf.RoundToInt(((damage * 3) + (((float)this.weapon.maxWeapon() / 5) * (this.attributes.strength - 15)) + hitPoints) * modificator);
    public int minArrow() => (this.arrow != null) ? this.arrow.damage.item1 : 0;
    public int maxArrow() => (this.arrow != null) ? this.arrow.damage.item2 : 0;
    public void TakeItem(Item i)
    {
        this.inv.TakeItem(i);
    }
    public Consumable dropGoldCoins(int value) => this.inv.RemoveGoldCoins(value);

    public Consumable dropSilverCoins(int value) => this.inv.removeSilverCoins(value);

    public Consumable dropCopperCoins(int value) => this.inv.removeCopperCoins(value);

    public int getGoldCoins() => this.inv.goldCoins;

    public int getSilverCoins() => this.inv.silverCoins;

    public int getCopperCoins() => this.inv.copperCoins;

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
        this.incrementHitPoints();
        this.lvl += 1;
        this.xpMax = this.xp * 2; //Reemplazar esta asignacion por otra que traiga el siguiente lvl de la base de datos
        this.xp = 0;
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
    public bool hasAmmunition() => this.arrow != null;
    public void DiscardAmmunition()
    {
        this.inv.RemoveItemByQuantity(this.arrow.name, 1);
        if(this.arrow.quantity - 1 <= 0)
        {
            this.arrow = null;
        }
    }
    public void incrementHitPoints() //Usar polimorfismo!!
    {
        this.hitPoints.item1 = this.clasf.nextHitPoints(this.lvl, this.hitPoints.item1);
        this.hitPoints.item2 = this.clasf.nextHitPoints(this.lvl, this.hitPoints.item2);
    }
    public void Steal(Player other)
    {
        var randomNumber = Random.Range(0, 100);
        var stealChance = this.clasf.stealChance(this.skills.steal);
        Debug.Log("Numero random para robar: " + randomNumber);
        Debug.Log("Chance de robar: " + stealChance);
        if (randomNumber < stealChance)
        {
            this.clasf.Steal(this, other);
        }
    }

    //Funcion creada para la skill de supervivencia, pero faltaria agregar que aumente la regeneracion de energia
    public string examineLifePointsOf(Player ch)
    {
        switch (this.skills.survival)
        {
            case int n when n >= 0 && n <= 10:
                return this.examine(ch, 0);
            case int n when n >= 11 && n <= 20:
                return this.examine(ch, 1);
            case int n when n >= 21 && n <= 30:
                return this.examine(ch, 2);
            case int n when n >= 31 && n <= 40:
                return this.examine(ch, 3);
            case int n when n >= 41 && n <= 60:
                return this.examine(ch, 4);
            case int n when n >= 60:
                return this.examine(ch, -1);
            default:
                throw new System.Exception("Cantidad de skills invalida");
        }
    }

    private string examine(Player ch, int discriminator)
    {
        switch (ch.state.lifePoints)
        {
            case int cl when discriminator == 0:
                return "Dudoso";
            case int cl when discriminator == 1:
                return (cl >= ch.state.maxLifePoints * 0.5) ? "Sano" : "Herido";
            case int cl when discriminator == 2 && cl >= ch.state.maxLifePoints * 0.5f && cl <= ch.state.maxLifePoints * 0.75f:
                return "Herido";
            case int cl when discriminator == 2 && cl <= ch.state.maxLifePoints * 0.5f:
                return "Gravemente Herido";
            case int cl when discriminator == 4 && cl == ch.state.maxLifePoints * 1:
                return "Intacto";
            case int cl when discriminator >= 2 && cl >= ch.state.maxLifePoints * 0.75f:
                return "Sano";
            case int cl when discriminator == 3 && cl <= ch.state.maxLifePoints * 0.25f:
                return "Gravemente Herido";
            case int cl when discriminator >= 3 && cl >= ch.state.maxLifePoints * 0.5f && cl <= ch.state.maxLifePoints * 0.75f:
                return "Levemente Herido";
            case int cl when discriminator >= 3 && cl >= ch.state.maxLifePoints * 0.25f && cl <= ch.state.maxLifePoints * 0.5f:
                return "Herido";
            case int cl when discriminator == 4 && cl >= ch.state.maxLifePoints * 0.1f && cl <= ch.state.maxLifePoints * 0.25f: 
                return "Gravemente Herido";
            case int cl when discriminator == 4 && cl <= ch.state.maxLifePoints * 0.1f:
                return "Casi Muerto";
            default:
                return "(" + ch.state.lifePoints + "/" + ch.state.maxLifePoints + ")";
        }
    }
    public void ModifyState() { } //Para la magia de paralizar ¿?
    public int resourcesObtained() => this.clasf.resourcesObtained(this.lvl);
    public int extractionChance(int skillPoints)
    { 
        var extractionPercentage = new List<int>() { 12, 13, 14, 15, 17, 20, 23, 28, 35, 55, 100 };
        switch (skillPoints.ToString().Length)
        {
            case 1:
                return extractionPercentage[0];
            case 2:
                return extractionPercentage[int.Parse(skillPoints.ToString()[0].ToString())];
            case 3:
                return extractionPercentage[10];
            default:
                throw new System.Exception("Skill fuera de los limites");
        }
    }

    public void CatchFish(FountainOfResources res)
    {
        this.SubstractResources(res, this.extractionChance(this.skills.fishing));

    }
    public void Cutdown(FountainOfResources res)
    {
        this.SubstractResources(res, this.extractionChance(this.skills.cutDownTrees));
    }
    public void ExtractRoots(FountainOfResources res)
    {
        this.SubstractResources(res, this.extractionChance(this.skills.botany));
    }
    public void Mine(FountainOfResources res)
    {
        this.SubstractResources(res, this.extractionChance(this.skills.mining));
    }
    public void SubstractResources(FountainOfResources res, int chanceOfSuccess)
    {
        var obtained = this.resourcesObtained(); 
        Item fromFountain = tool.whatSubstract(res, Random.Range(0, 21), res.resources(obtained)); //Esta funcion es la que levanta un exception si no se esta usando la herramienta adecuada para el trabajo.
        if (Random.Range(0, 101) <= chanceOfSuccess)
        {
            this.TakeItem(fromFountain);
            res.substractResources(obtained);
        }
    }
    public void ExtractResource(FountainOfResources res)
    {
        res.HowToSubstract(this);
    }

}



