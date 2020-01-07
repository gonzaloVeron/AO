using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public int lifePoints;
    public int maxLifePoints;
    public int manaPoints;
    public int maxManaPoints;
    public int energyPoints;
    public int maxEnergyPoints;
    public int hungryPoints;
    public int maxHungryPoints;
    public int thirstPoints;
    public int maxThirstPoints;

    public State(int lifePoints, int manaPoints, int energyPoints, int hungryPoints, int thirstPoints)
    {
        this.lifePoints = lifePoints;
        this.maxLifePoints = lifePoints;
        
        this.manaPoints = manaPoints;
        this.maxManaPoints = manaPoints;
        
        this.energyPoints = energyPoints;
        this.maxEnergyPoints = energyPoints;
        
        this.hungryPoints = hungryPoints;
        this.maxHungryPoints = hungryPoints;

        this.thirstPoints = thirstPoints;
        this.maxThirstPoints = thirstPoints;
    }

}
