using System;
using System.Collections.Generic;
using UnityEngine;

public class TheAnimalWasNotTamedException : Exception
{
    public TheAnimalWasNotTamedException(string animalName) : base("El animal " + animalName + " no pudo ser domado.") { }
}
