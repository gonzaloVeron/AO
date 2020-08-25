public class NoShield : Shield
{
    private readonly static NoShield _instance = new NoShield();

    private NoShield() : base("Sin escudo", 0, 0, 0, 0, 0, 0) { }

    public static NoShield Instance
    {
        get
        {
            return _instance;
        }
    }

    public override bool isShield() => false;

    public override int shieldingMod() => 0;
}
