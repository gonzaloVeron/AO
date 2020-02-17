using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    public State state;


    public abstract void Attack(Character other);
    public abstract int probabilidadDeAcierto(int skill, int agility, Character other);
    public abstract float evasion();
}
