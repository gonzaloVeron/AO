using System;

public class FunctionNotSupported : Exception
{
    public FunctionNotSupported(string message) : base(message) { }
}
