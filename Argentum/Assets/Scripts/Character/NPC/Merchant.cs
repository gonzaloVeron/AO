using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : Character
{
    // No tocar por el momento
    public override void Attack(Character other)
    {
        throw new System.NotImplementedException();
    }

    public override float evasion()
    {
        throw new System.NotImplementedException();
    }

    public override int physicalDefense()
    {
        throw new System.NotImplementedException();
    }

    public override int successProbability(Character other)
    {
        throw new System.NotImplementedException();
    }
}
