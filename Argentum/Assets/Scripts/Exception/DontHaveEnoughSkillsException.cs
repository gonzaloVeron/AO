using System;
using System.Collections.Generic;
using UnityEngine;

public class DontHaveEnoughSkillsException : Exception
{
    public DontHaveEnoughSkillsException(string skillName) : base("No tienes suficientes puntos de " + skillName + ".") { }
}
