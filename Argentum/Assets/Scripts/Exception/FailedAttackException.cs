using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FailedAttackException : Exception
{
    public FailedAttackException(string playerName) : base("El ataque de " + playerName + " fallo.") { }
}
