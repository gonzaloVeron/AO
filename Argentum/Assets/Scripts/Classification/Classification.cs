using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Falta nigromante, pirata, gladiador, cazador
public abstract class Classification
{
    public List<float> stabbingPercentage = new List<float>() { 0.8f, 1.6f, 2.4f, 3.2f, 4f, 4.8f, 5.6f, 6.4f, 7.2f, 8f };
    public List<float> criticalPercentage = new List<float>();
    public abstract int calculateLifePoints(int constitution);
    public abstract int calculateManaPerLevel(int intelligence);
    public abstract int initialMana();
    public abstract int hitPointsPerLevel(int characterLvl);
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
    public virtual float stabChance(int skill) => this.calculateChance(skill, this.stabbingPercentage);
    public virtual float critChance(int skill) => 0f;
    public virtual void Attack(Character self, Character other)
    {
        other.BeingAttacked(self.damage());
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
}
