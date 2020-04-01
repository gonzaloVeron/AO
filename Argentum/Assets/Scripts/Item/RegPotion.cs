using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegPotion : Potion
{
    public RegPotion(int quantity, float weight) : base("Pocion de regeneracion", 0, 0, quantity, weight) { }

    public override void Use(Player other)
    {
        other.Heal(Mathf.RoundToInt(other.state.maxLifePoints * 0.1f));
        other.HealMana(Mathf.RoundToInt(other.state.maxManaPoints * 0.1f));
    }
}