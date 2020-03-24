using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : Classification
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
    public override float defenseEvasionMod() => 1.1f;
    public override float defenseShieldMod() => 0f;
    public override int hitPointsPerLevel(int characterLvl) => 3;
    public override float meleeAimMod() => 0.9f;
    public override float meleeDamageMod() => 0.8f;
    public override float projectileWeaponAimMod() => 1f;
    public override float projectileWeaponDamageMod() => 0.95f;
    public override float withoutWeaponAimMod() => 1f;
    public override float withoutWeaponDamageMod() => 1.075f;
    public override float magicalDamageMod() => 0f;
    public override float stealChance(int skill) => this.calculateChance(skill, this.stealPercentage().ConvertAll(n => n * 3));
    public override void Steal(Player thief, Player victim)
    {
        var randomNumber = Random.Range(0, 100);
        var chanceToStealItem = base.stealChance(thief.skills.steal);
        Debug.Log("Numero random para robar item: " + randomNumber);
        Debug.Log("Chance de robar item: " + chanceToStealItem);
        if (randomNumber < chanceToStealItem)
        {
            var objectStealed = victim.inv.getRandomItem().copy();
            objectStealed.quantity = (objectStealed.quantity == 1) ? 1 : Mathf.RoundToInt(objectStealed.quantity / 2);
            victim.inv.RemoveItemByQuantity(objectStealed.name, objectStealed.quantity);
            thief.TakeItem(objectStealed);
        }
        else
        {
            base.Steal(thief, victim);
        }
    }
}
