public class CopperCoin : Coin
{
    public CopperCoin(int value) : base("Monedas de cobre", value) { }

    public override void BeTaked(Inventory inv)
    {
        inv.copperCoins += this.quantity;
    }
}
