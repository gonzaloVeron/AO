using System;
using System.Collections.Generic;
using UnityEngine;

public class InvalidResourceException : Exception
{
    public InvalidResourceException(string resourceName) : base(resourceName + " no es un recurso valido.") { }
}
