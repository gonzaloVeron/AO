using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : Classification
{
    public override int calculateLifePointsPerLevel(int constitution)
    {
        switch (constitution)
        {
            case 18:
                return Random.Range(5, 10);
            case 19:
                return Random.Range(6, 10);
            case 20:
                return Random.Range(6, 11);
            case 21:
                return Random.Range(7, 11);
            default:
                throw new System.Exception("Calculo de vida con una constitucion incorrecta");
        }
    }
    public override int calculateManaPerLevel(int intelligence) => intelligence * 2;
    public override int initialMana() => 50;
    public override float defenseEvasionMod() => 0.6f; //Estaba en 0.75f revisar si todos los mods del excel son iguales a los de las clases
    public override float defenseShieldMod() => 0f;
    public override int hitPointsPerLevel(int characterLvl) => 2;
    public override float meleeAimMod() => 0.65f;
    public override float meleeDamageMod() => 0.7f;
    public override float projectileWeaponAimMod() => 0.7f;
    public override float projectileWeaponDamageMod() => 0.75f;
    public override float withoutWeaponAimMod() => 0.4f;
    public override float withoutWeaponDamageMod() => 0.4f;
    public override float magicalDamageMod() => 0.7f;
    public override float tameAnimalMod() => 1f;
}
