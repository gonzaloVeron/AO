using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Classification
{
    public new List<float> criticalPercentage = new List<float>() { 1.9f, 3.8f, 5.7f, 7.6f, 9.5f, 11.4f, 13.3f, 15.2f, 17.2f, 19f };
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
    public override int calculateManaPerLevel(int intelligence) => Mathf.RoundToInt(intelligence / 3 * 2);
    public override int initialMana() => 50;
    public override float defenseEvasionMod() => 0.7f;
    public override float defenseShieldMod() => 2f;
    public override int hitPointsPerLevel(int characterLvl) => characterLvl <= 36 ? 3 : 1;
    public override float meleeAimMod() => 0.85f;
    public override float meleeDamageMod() => 0.77f;
    public override float projectileWeaponAimMod() => 0.8f;
    public override float projectileWeaponDamageMod() => 0.7f;
    public override float withoutWeaponAimMod() => 0.95f;
    public override float withoutWeaponDamageMod() => 1.05f;
    public override int critDamage(int dmg) => dmg + Mathf.RoundToInt(dmg * 0.75f);
    public override float critChance(int skill) => this.calculateChance(skill, this.criticalPercentage);
    public override void Attack(Character self, Character other)
    {
        if (Random.Range(0f, 101f) <= this.critChance(self.skills.armedCombat))
        {
            other.BeingAttacked(this.critDamage(self.damage()));
        }
        else
        {
            other.BeingAttacked(self.damage());
        }
    }
}
