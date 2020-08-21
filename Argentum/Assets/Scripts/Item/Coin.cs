using System.Collections;
using System.Collections.Generic;

public abstract class Coin : Consumable
{
    public Coin(string name, int value) : base(name, 0, 0, 0, 0, 0, value, 0f) { }

}
