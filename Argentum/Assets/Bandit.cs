using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Classification
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
        throw new System.NotImplementedException();
    }

    public override float defenseEvasionMod()
    {
        throw new System.NotImplementedException();
    }

    public override float defenseShieldMod()
    {
        throw new System.NotImplementedException();
    }

    public override int hitPointsPerLevel(int characterLvl)
    {
        throw new System.NotImplementedException();
    }

    public override float meleeAimMod()
    {
        throw new System.NotImplementedException();
    }

    public override float meleeDamageMod()
    {
        throw new System.NotImplementedException();
    }

    public override float projectileWeaponAimMod()
    {
        throw new System.NotImplementedException();
    }

    public override float projectileWeaponDamageMod()
    {
        throw new System.NotImplementedException();
    }

    public override float withoutWeaponAimMod()
    {
        throw new System.NotImplementedException();
    }

    public override float withoutWeaponDamageMod()
    {
        throw new System.NotImplementedException();
    }
}
