using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Equipable
{
    public Helmet(string name, int minHelmet, int maxHelmet, int quantity, float weight) : base(name, quantity, weight)
    {
        this.helmet = new Tuple<int, int>(minHelmet, maxHelmet);
    }

    public int minHelmet() => this.helmet.item1;

    public int maxHelmet() => this.helmet.item2;

}
