using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingMan : Classification
{
    public override int calculateLifePointsPerLevel(int constitution)
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

    public override int calculateManaPerLevel(int intelligence) => 0;
    public override int initialMana() => 0;
    public override float defenseEvasionMod() => 0.8f;
    public override float defenseShieldMod() => 0.85f;
    public override int hitPointsPerLevel(int characterLvl) => 2;
    public override float meleeAimMod() => 0.9f;
    public override float meleeDamageMod() => 0.8f;
    public override float projectileWeaponAimMod() => 0.9f;
    public override float projectileWeaponDamageMod() => 0.8f;
    public override float withoutWeaponAimMod() => 0.9f;
    public override float withoutWeaponDamageMod() => 0.8f;
    public override float magicalDamageMod() => 0f;
}
