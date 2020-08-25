public sealed class NoWeapon : Knuckles
{
    private readonly static NoWeapon _instance = new NoWeapon();

    private NoWeapon() : base("Sin Arma", 0, 0, 0, 0) { }

    public static NoWeapon Instance
    {
        get
        {
            return _instance;
        }
    }
}
