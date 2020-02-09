using System;

public class NonCopyableItemException : Exception
{
    public NonCopyableItemException(string itemName) : base("El item: " + itemName + " no se puede copiar.") { }
}
