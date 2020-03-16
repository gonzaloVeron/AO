using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Character
{
    public int creatureDefense;
    public int creatureAim;
    public int creatureEvasion;
    public int exp;
    public List<Item> drop;

    public Creature(string name, int lifePoints, Tuple<int, int> hitPoints,int creatureDefense, int creatureAim, int creatureEvasion, int exp, List<Item> drop)
    {
        this.name = name;
        this.state = new State(lifePoints, 0, 0, 0, 0);
        this.hitPoints = new Tuple<int, int>(hitPoints.item1, hitPoints.item2);
        this.creatureDefense = creatureDefense;
        this.creatureAim = creatureAim;
        this.creatureEvasion = creatureEvasion;
        this.exp = exp;
        this.drop = drop;
    }

    public override void Attack(Character other)
    {
        var prob = this.successProbability(other);
        var probRand = Random.Range(0, 101);
        Debug.Log("Probabilidad de acierto de la criatura: " + prob);
        Debug.Log("Numero random: " + probRand);
        if (probRand <= prob)
        {
            Debug.Log("La criatura no fallo !");
            other.BeingAttacked(Random.Range(this.hitPoints.item1, this.hitPoints.item2 + 1));
        }
        else
        {
            throw new FailedAttackException(this.name);
        }
    }
    public override int successProbability(Character other) => Mathf.Max(10, Mathf.Min(90, 50 + Mathf.RoundToInt(0.4f * (this.creatureAim - other.evasion()))));
    public override int physicalDefense() => this.creatureDefense;
    public override float evasion() => this.creatureEvasion;
    public virtual void BeingTamed(Player player)
    {
        throw new CantTameCreaturesException(this.name);
    }
}
