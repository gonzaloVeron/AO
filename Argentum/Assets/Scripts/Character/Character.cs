using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    public string name;
    public State state;

    public Tuple<int, int> hitPoints;

    public abstract void Attack(Character other);
    public abstract int successProbability(Character other);
    public virtual void BeingAttacked(int value)
    {
        this.state.lifePoints = Mathf.Max(0, this.state.lifePoints - Mathf.Max(0, value - this.physicalDefense()));
    }
    public abstract int physicalDefense();
    public abstract float evasion();
}
