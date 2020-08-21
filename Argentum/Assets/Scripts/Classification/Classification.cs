using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;
//Falta nigromante, pirata, gladiador, cazador
[BsonKnownTypes(typeof(Classification), typeof(Assassin), typeof(Bandit), typeof(Bard), typeof(Cleric), typeof(Druid), typeof(Paladin), typeof(Thief), typeof(Warrior), typeof(Wizard), typeof(WorkingMan))]
public abstract class Classification
{
    public virtual List<float> criticalPercentage() => new List<float>();
    public virtual List<float> stealPercentage() => new List<float>() { 2f, 4f, 6f, 8f, 10f, 12f, 14f, 16f, 18f, 20f };
    public virtual List<float> stabbingPercentage() => new List<float>() { 0.8f, 1.6f, 2.4f, 3.2f, 4f, 4.8f, 5.6f, 6.4f, 7.2f, 8f };
    //---
    public abstract int calculateLifePointsPerLevel(int constitution);
    public abstract int calculateManaPerLevel(int intelligence);
    public abstract int initialMana();
    public abstract int hitPointsPerLevel(int characterLvl);
    public virtual int nextHitPoints(int characterLvl, int hitPoints) => (characterLvl < 35) ? Mathf.Min(99, hitPoints + this.hitPointsPerLevel(characterLvl)) : hitPoints + this.hitPointsPerLevel(characterLvl);
    public abstract float meleeDamageMod();
    public abstract float meleeAimMod();
    public abstract float projectileWeaponDamageMod();
    public abstract float projectileWeaponAimMod();
    public abstract float withoutWeaponDamageMod();
    public abstract float withoutWeaponAimMod();
    public abstract float defenseShieldMod();
    public abstract float defenseEvasionMod();
    public int energyPerLevel() => 15;
    public virtual int critDamage(int dmg) => dmg;
    public virtual int stabDamage(int dmg) => dmg + Mathf.RoundToInt(dmg * 1.5f);
    public virtual float stabChance(int skill) => this.calculateChance(skill, this.stabbingPercentage());
    public virtual float critChance(int skill) => 0f;
    public virtual void Attack(Player self, Character other)
    {
        other.BeingAttacked(self.damage());
    }
    public virtual void AttackWithBow(Player self, Character other)
    {
        other.BeingAttacked(self.damageWithBow());
    }
    protected float calculateChance(int skill, List<float> chances)
    {
        switch (skill.ToString().Length)
        {
            case 1:
                return chances[0];
            case 2:
                return chances[skill.ToString()[0]];
            case 3:
                return chances[9];
            default:
                throw new System.Exception("Skill fuera de los limites");
        }
    }
    public virtual float stealChance(int skill) => this.calculateChance(skill, this.stealPercentage());
    public abstract float magicalDamageMod();
    public virtual void Steal(Player thief, Player victim)
    {
        int goldStealed = Mathf.RoundToInt(victim.getGoldCoins() * 0.03f);
        thief.TakeItem(victim.dropGoldCoins(goldStealed)); //dropGoldCoins devuelve el oro robado a la victima 
    }
    public virtual float tameAnimalMod() => 0.35f;
    public virtual int resourcesObtained(int lvl) => 1;
}
