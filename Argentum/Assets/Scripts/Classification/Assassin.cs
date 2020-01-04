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

    public override int calculateManaPerLevel(int intelligence)
    {
        return intelligence;
    }
    public override int initialMana()
    {
        return 50;
    }
    public override float defenseEvasionMod()
    {
        return 1.1f;
    }

    public override float defenseShieldMod()
    {
        return 0.8f;
    }

    public override int hitPointsPerLevel(int characterLvl)
    {
        return characterLvl <= 36 ? 3 : 1; 
    }

    public override float meleeAimMod()
    {
        return 0.9f;
    }

    public override float meleeDamageMod()
    {
        return 0.9f;
    }

    public override float projectileWeaponAimMod()
    {
        return 0.75f;
    }

    public override float projectileWeaponDamageMod()
    {
        return 0.8f;
    }

    public override float withoutWeaponAimMod()
    {
        return 0.9f;
    }

    public override float withoutWeaponDamageMod()
    {
        return 0.9f;
    }
}
