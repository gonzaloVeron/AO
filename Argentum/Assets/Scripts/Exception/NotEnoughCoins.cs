using System;

public class NotEnoughCoins : Exception
{
    public NotEnoughCoins(string message) : base(message) { }
}
