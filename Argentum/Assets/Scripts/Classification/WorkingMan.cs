using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingMan : Classification
{
    public override int calculateLifePoints(int constitution)
    {
        switch (constitution)
        {
            case 18:
                return Random.Range(6, 11);
            case 19:
                return Random.Range(7, 11);
            case 20:
                return Random.Range(7, 12);
            case 21:
                return Random.Range(8, 12);
            default:
                throw new System.Exception("Calculo de vida con una constitucion incorrecta");
        }
    }

    public override int calculateManaPerLevel(int intelligence)
    {
        return 0;
    }

    public override int initialMana()
    {
        return 0;
    }

    public override float defenseEvasionMod()
    {
        return 0.8f;
    }

    public override float defenseShieldMod()
    {
        return 0.85f;
    }

    public override int hitPointsPerLevel(int characterLvl)
    {
        return 2;
    }

    public override float meleeAimMod()
    {
        return 0.9f;
    }

    public override float meleeDamageMod()
    {
        return 0.8f;
    }

    public override float projectileWeaponAimMod()
    {
        return 0.9f;
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
        return 0.8f;
    }
}
