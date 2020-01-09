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
}
