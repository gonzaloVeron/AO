using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Classification
{
    public override int calculateLifePointsPerLevel(int constitution)
    {
        switch (constitution)
        {
            case 18:
                return Random.Range(4, 9);
            case 19:
                return Random.Range(5, 9);
            case 20:
                return Random.Range(5, 10);
            case 21:
                return Random.Range(6, 10);
            default:
                throw new System.Exception("Calculo de vida con una constitucion incorrecta");
        }
    }

    public override int calculateManaPerLevel(int intelligence) => Mathf.RoundToInt(intelligence * 3.74f - 20);
    public override int initialMana() => 70;
    public override float defenseEvasionMod() => 0.4f;
    public override float defenseShieldMod() => 0f;
    public override int hitPointsPerLevel(int characterLvl) => 1;
    public override float meleeAimMod() => 0.5f;
    public override float meleeDamageMod() => 0.7f;
    public override float projectileWeaponAimMod() => 0.5f;
    public override float projectileWeaponDamageMod() => 0.5f;
    public override float withoutWeaponAimMod() => 0.3f;
    public override float withoutWeaponDamageMod() => 0.4f;
    public override float magicalDamageMod() => 1f;
}
