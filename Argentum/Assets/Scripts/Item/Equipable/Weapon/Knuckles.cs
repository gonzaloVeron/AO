public class Knuckles : Weapon
{
    public Knuckles(string name, int minWeapon, int maxWeapon, int quantity, float weight) : base(name, quantity, weight, 0, 0)
    {
        this.weapon = new Tuple<int, int>(minWeapon, maxWeapon);
    }
    public override Tuple<int, int> calculateDamage(Player self) => self.weapon.weapon;
    public override float damageMod(Classification clasf) => clasf.withoutWeaponDamageMod();
    public override float modForWeapon(Classification clasf) => clasf.withoutWeaponAimMod();
    public override int requiredSkill(Skills sk) => sk.martialArts;
    public override Item copy() => new Knuckles(this.name, this.weapon.item1, this.weapon.item2, this.quantity, this.weight);
}
