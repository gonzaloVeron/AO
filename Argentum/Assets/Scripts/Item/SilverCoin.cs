public class SilverCoin : Coin
{
    public SilverCoin(int value) : base("Monedas de plata", value) { }

    public override void BeTaked(Inventory inv)
    {
        inv.silverCoins += this.quantity;
    }
}
