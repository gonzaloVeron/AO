using System;

public class NotEnoughMana : Exception
{
    public NotEnoughMana(string message) : base(message) { }
}
