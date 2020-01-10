using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Classification
{

    public override int calculateLifePoints(int constitution)
    {
        switch (constitution)
        {
            case 18:
                return Random.Range(5, 10); //min inclusive, max exclusive se requeria 5/9
            case 19:
                return Random.Range(6, 10); //min inclusive, max exclusive se requeria 6/9
            case 20:
                return Random.Range(6, 11); //min inclusive, max exclusive se requeria 6/10
            case 21:
                return Random.Range(7, 11); //min inclusive, max exclusive se requeria 7/10
            default:
                throw new System.Exception("Calculo de vida con una constitucion incorrecta");
        }
    }

    public override int calculateManaPerLevel(int intelligence) => intelligence;
    public override int initialMana() => 50;
    public override float defenseEvasionMod() => 1.1f;
    public override float defenseShieldMod() => 0.8f;
    public override int hitPointsPerLevel(int characterLvl) => characterLvl <= 36 ? 3 : 1; 
    public override float meleeAimMod() => 0.9f;
    public override float meleeDamageMod() => 0.9f;
    public override float projectileWeaponAimMod() => 0.75f;
    public override float projectileWeaponDamageMod() => 0.8f;
    public override float withoutWeaponAimMod() => 0.9f;
    public override float withoutWeaponDamageMod() => 0.9f;
    public override int stabDamage(int dmg) => dmg + Mathf.RoundToInt(dmg * 1.4f);
    public override float stabChance(int skill)
    {
        switch (skill)
        {
            case int n when n >= 0 && n <= 10:
                return 2.4f;
            case int n when n >= 11 && n <= 20:
                return 4.8f;
            case int n when n >= 21 && n <= 30:
                return 7.2f;
            case int n when n >= 31 && n <= 40:
                return 9.6f;
            case int n when n >= 41 && n <= 50:
                return 12f;
            case int n when n >= 51 && n <= 60:
                return 14.4f;
            case int n when n >= 61 && n <= 70:
                return 16.8f;
            case int n when n >= 71 && n <= 80:
                return 19.2f;
            case int n when n >= 81 && n <= 90:
                return 21.6f;
            case int n when n >= 91 && n <= 100:
                return 24f;
            default:
                throw new System.Exception("Skill fuera de los limites");
        }
    }
}
