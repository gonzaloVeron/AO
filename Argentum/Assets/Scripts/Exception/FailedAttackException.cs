using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FailedAttackException : Exception
{
    public FailedAttackException(string characterName) : base("El ataque de " + characterName + " fallo") { }
}
