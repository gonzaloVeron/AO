using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public int lifePoints;
    public int manaPoints;
    public int hungryPoints;
    public int thirstPoints;

    public State(int lifePoints, int manaPoints, int hungryPoints, int thirstPoints)
    {
        this.lifePoints = lifePoints;
        this.manaPoints = manaPoints;
        this.hungryPoints = hungryPoints;
        this.thirstPoints = thirstPoints;
    }

}
