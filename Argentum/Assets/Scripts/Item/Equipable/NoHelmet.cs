public class NoHelmet : Helmet
{
    private readonly static NoHelmet _instance = new NoHelmet();

    private NoHelmet() : base("Sin casco", 0, 0, 0, 0, 0, 0) { }

    public static NoHelmet Instance
    {
        get
        {
            return _instance;
        }
    }
}
