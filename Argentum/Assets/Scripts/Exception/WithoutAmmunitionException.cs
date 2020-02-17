using System;
using UnityEngine;

public class WithoutAmmunitionException : Exception
{
    public WithoutAmmunitionException(string playerName) : base("El personaje: " + playerName + " no tiene municion") { }
}
