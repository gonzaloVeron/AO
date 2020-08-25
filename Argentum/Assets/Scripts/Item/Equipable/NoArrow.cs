public class NoArrow : Arrow
{
    private readonly static NoArrow _instance = new NoArrow();

    private NoArrow() : base("Sin flechas", 0, 0, 0, 0) { }

    public static NoArrow Instance
    {
        get
        {
            return _instance;
        }
    }

    public override bool isArrow() => false;
}
