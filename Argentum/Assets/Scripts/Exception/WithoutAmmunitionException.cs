using System;
using UnityEngine;

public class WithoutAmmunitionException : Exception
{
    public WithoutAmmunitionException(string characterName) : base("El personaje: " + characterName + " no tiene municion") { }
}
