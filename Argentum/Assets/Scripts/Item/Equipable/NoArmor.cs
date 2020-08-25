public sealed class NoArmor : Armor
{
    private readonly static NoArmor _instance = new NoArmor();

    private NoArmor() : base("Sin armadura", 0, 0, 0, 0, 0, 0){ }

    public static NoArmor Instance
    {
        get
        {
            return _instance;
        }
    }

}
