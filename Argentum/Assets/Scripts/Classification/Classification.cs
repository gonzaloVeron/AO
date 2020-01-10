using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Falta nigromante, pirata, gladiador, cazador
public abstract class Classification
{
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
    public virtual int stabDamage(int dmg) => dmg + Mathf.RoundToInt(dmg * 1.5f);
    public virtual float stabChance(int skill)
    {
        switch(skill)
        {
            case int n when n >= 0 && n <= 10:
                return 0.8f;
            case int n when n >= 11 && n <= 20:
                return 1.6f;
            case int n when n >= 21 && n <= 30:
                return 2.4f;
            case int n when n >= 31 && n <= 40:
                return 3.2f;
            case int n when n >= 41 && n <= 50:
                return 4f;
            case int n when n >= 51 && n <= 60:
                return 4.8f;
            case int n when n >= 61 && n <= 70:
                return 5.6f;
            case int n when n >= 71 && n <= 80:
                return 6.4f;
            case int n when n >= 81 && n <= 90:
                return 7.2f;
            case int n when n >= 91 && n <= 100:
                return 8f;
            default:
                throw new System.Exception("Skill fuera de los limites");
        }
    }
}
