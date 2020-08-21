public class GoldCoin : Coin
{
    public GoldCoin(int value) : base("Monedas de oro", value) { }

    public override void BeTaked(Inventory inv)
    {
        inv.goldCoins += this.quantity;
    }
}
