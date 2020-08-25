using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Equipable
{
    public Shield(string name, int minShield, int maxShield, int magicalDefense, int magicalDamage, int quantity, float weight) : base(name, quantity, weight, magicalDefense, magicalDamage)
    {
        this.shield = new Tuple<int, int>(minShield, maxShield);
    }

    public int minShield() => this.shield.item1;
    public int maxShield() => this.shield.item2;
    public override Item copy() => new Shield(this.name, this.shield.item1, this.shield.item2, this.magicalDefense, this.magicalDamage, this.quantity, this.weight);
    public override void Use(Player other)
    {
        if (other.shield != this)
        {
            other.shield = this;
            other.weight += this.weight;
        }
        else
        {
            other.shield = NoShield.Instance;
            other.weight -= this.weight;
        }
    }

    public virtual bool isShield() => true;

    //Al agregar la clase "NoShield" el calculo (this.shield != null) ? this.skills.shieldDefese * 0.5f * this.clasf.defenseShieldMod() : 0f; ya no tiene sentido de existir
    //Esta funcion solo existe para evitar usar if, se trata de que si tiene escudo entonces
    //se tiene que calcular el porcentaje de escudeo que es ajeno al escudo, 1 significa que tiene escudo y 0 no
    public virtual int shieldingMod() => 1; 
}
