using System;
using System.Collections.Generic;
using UnityEngine;

public class CantTameCreaturesException : Exception
{
    public CantTameCreaturesException(string creatureName) : base("No se puede domar al: " + creatureName + ".") { }
}
