using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public int lifePoints;
    public int manaPoints;
    public int manaPointsMax;
    public int energyPoints;
    public int hungryPoints;
    public int thirstPoints;

    public State(int lifePoints, int manaPoints, int energyPoints, int hungryPoints, int thirstPoints)
    {
        this.lifePoints = lifePoints;
        this.manaPoints = manaPoints;
        this.manaPointsMax = manaPoints;
        this.energyPoints = energyPoints;
        this.hungryPoints = hungryPoints;
        this.thirstPoints = thirstPoints;
    }

}
