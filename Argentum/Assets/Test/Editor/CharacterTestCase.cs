using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class CharacterTestCase
{
    private Character spore;

    private Character other;

    private Attributes attributesSpore;

    private Attributes attributesOther;

    private Skills skillsSpore;

    private Skills skillsOther;

    [SetUp]
    public void SetUp()
    {
        attributesSpore = new Attributes(12, 0, 0, 0, 0, 0, 0);
        attributesOther = new Attributes(6, 0, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        skillsOther = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        spore = new Character("Spore", attributesSpore, skillsSpore);
        other = new Character("Jorge", attributesOther, skillsOther);
    }

    [Test]
    public void AttackTest()
    {
        var experienceExpected = 2;
        var lifeExpected = 88;
        spore.Attack(other);
        Assert.AreEqual(lifeExpected, other.state.lifePoints);
        Assert.AreEqual(experienceExpected, spore.xp);
    }

    [Test]
    public void BeingAttacked()
    {
        var lifeExpected = 95;
        spore.BeingAttacked(5);
        Assert.AreEqual(lifeExpected, spore.state.lifePoints);
    }
    
    [Test]
    public void DamageTest()
    {
        var damageExpected = 12;
        Assert.AreEqual(damageExpected, spore.damage());
    }

    [Test]
    public void TakeItemGold()
    {
        var goldExpected = 55;
        var gold = new Consumable("Gold", 0, 0, 0, 0, 0, goldExpected, 1);
        spore.TakeItem(gold);
        Assert.AreEqual(goldExpected, spore.gold);
    }

    [Test]
    public void TakeItemExistingItem()
    {
        var inventoryQuantityExpected = 1;
        var redPotionQuantityExpected = 7;
        var item1 = new Consumable("Pocion Roja", 30, 0, 0, 0, 0, 0, 3);
        var item2 = new Consumable("Pocion Roja", 30, 0, 0, 0, 0, 0, 4);
        spore.TakeItem(item1);
        spore.TakeItem(item2);
        Assert.AreEqual(inventoryQuantityExpected, spore.inv.itemsAmount());
        Assert.AreEqual(redPotionQuantityExpected, spore.inv.fetchItem("Pocion Roja").quantity);

    }

    [Test]
    public void TakeItemNewItem()
    {
        var inventoryQuantityExpected = 2;
        var item1 = new Consumable("Pocion Azul", 0, 0.4f, 0, 0, 0, 0, 4);
        var item2 = new Equipable("Espada Larga", 0, 0, 0, 0, 0, 0, 4, 8, 1);
        spore.TakeItem(item1);
        spore.TakeItem(item2);
        Assert.AreEqual(inventoryQuantityExpected, spore.inv.itemsAmount());
    }

    [Test]
    public void DropGoldTest()
    {
        var nameExpected = "Gold";
        var expectedAmount = 5;
        var gold = new Consumable("Gold", 0, 0, 0, 0, 0, 66, 1);
        spore.TakeItem(gold);
        var goldDropped = spore.dropGold(5);
        Assert.AreEqual(nameExpected, goldDropped.name);
        Assert.AreEqual(expectedAmount, goldDropped.gold);
    }

    [Test]
    public void LearnSpellTest()
    {
        var spellsAmountExpected = 2;
        var spell1 = new Spell();
        var spell2 = new Spell();
        spore.LearnSpell(spell1);
        spore.LearnSpell(spell2);
        Assert.AreEqual(spellsAmountExpected, spore.spells.Count);
    }

    [Test]
    public void GainExperienceLevelUpTest()
    {
        var lvlExpected = 2;
        var experienceExpected = 0;
        spore.xpMax = 5;
        spore.GainExperience(52);
        Assert.AreEqual(experienceExpected, spore.xp);
        Assert.AreEqual(lvlExpected, spore.lvl);
    }

    [Test]
    public void GainExperienceTest()
    {
        var lvlExpected = 1;
        var experienceExpected = 60;
        spore.xpMax = 250;
        spore.GainExperience(60);
        Assert.AreEqual(experienceExpected, spore.xp);
        Assert.AreEqual(lvlExpected, spore.lvl);
    }

    [Test]
    public void NeedLvlUpTrueTest()
    {
        spore.xpMax = 55;
        spore.xp = 70;
        Assert.IsTrue(spore.needLevelUp(5));
    }

    [Test]
    public void NeedLvlUpFalseTest()
    {
        spore.xpMax = 55;
        spore.xp = 33;
        Assert.IsFalse(spore.needLevelUp(5));
    }

    [Test]
    public void LevelUpTest()
    {
        var experienceExpected = 0;
        var lvlExpected = 45;
        spore.xp = 9999;
        spore.lvl = 44;
        spore.LevelUp();
        Assert.AreEqual(lvlExpected, spore.lvl);
        Assert.AreEqual(experienceExpected, spore.xp);
    }

    [Test]
    public void BuyItemTest()
    {
        var goldExpected = 600;
        var itemsAmmount = 1;
        spore.gold = 55600;
        var itemToBuy = new Equipable("Espada de plata", 0, 0, 0, 0, 0, 0, 25, 29, 1);
        spore.BuyItem(55000, itemToBuy);
        Assert.AreEqual(goldExpected, spore.gold);
        Assert.AreEqual(itemsAmmount, spore.inv.itemsAmount());
    }

    [Test]
    public void SellItemTest()
    {
        var swordAmountExpected = 3;
        var goldExpected = 678000;
        var item = new Equipable("Espada MataDragones", 0, 0, 0, 0, 0, 0, 30, 35, 4);
        var item2 = new Equipable("Espada MataDragones", 0, 0, 0, 0, 0, 0, 30, 35, 1);
        spore.TakeItem(item);
        spore.SellItem(678000, item2);
        Assert.AreEqual(swordAmountExpected, spore.inv.inv.Find(i => i.name == item.name).quantity);
        Assert.AreEqual(goldExpected, spore.gold);
    }

    [Test]
    public void ChangeFactionTest()
    {
        var faction = new Faction();
        spore.ChangeFaction(faction);
        Assert.AreEqual(faction, spore.faction);
    }

    [Test]
    public void UseItemTest()
    {
        var lifePointsExpected = 130;
        var redPotionAmount = 3;
        var maxHelmetExpected = 5;
        var minHelmetExpected = 3;
        var potion = new Consumable("Red Potion", 30, 0, 0, 0, 0, 0, 4);
        var sword = new Equipable("Champ Helmet", 0, 0, 3, 5, 0, 0, 0, 0, 2);
        spore.TakeItem(potion);
        spore.TakeItem(sword);
        spore.UseItem("Red Potion");
        spore.UseItem("Champ Helmet");

        Assert.AreEqual(lifePointsExpected, spore.state.lifePoints);
        Assert.AreEqual(redPotionAmount, spore.inv.inv.Find(i => i.name == "Red Potion").quantity);
        Assert.AreEqual(maxHelmetExpected, spore.helmet.item2);
        Assert.AreEqual(minHelmetExpected, spore.helmet.item1);
    }

    [Test]
    public void DropItemTest()
    {
        var itemDropped1NameExpected = "Blue Potion";
        var itemDropped1AmountExpected = 7;
        var itemDropped2NameExpected = "Armor";
        var itemDropped2AmountExpected = 1;
        var itemDropped3NameExpected = "Chicken";
        var itemDropped3AmountExpected = 55;
        var sporeItemsAmount = 1;

        var item1 = new Consumable("Blue Potion", 0, 0.4f, 0, 0, 0, 0, 10);
        var item2 = new Consumable("Chicken", 0, 0, 5, 60, 0, 0, 55);
        var item3 = new Equipable("Armor", 30, 34, 0, 0, 0, 0, 0, 0, 1);

        spore.TakeItem(item1);
        spore.TakeItem(item2);
        spore.TakeItem(item3);

        var itemDropped1 = spore.dropItem("Blue Potion", 7);
        var itemDropped2 = spore.dropItem("Armor", 1);
        var itemDropped3 = spore.dropItem("Chicken", 80);

        Assert.AreEqual(itemDropped1NameExpected, itemDropped1.name);
        Assert.AreEqual(itemDropped1AmountExpected, itemDropped1.quantity);
        Assert.AreEqual(itemDropped2NameExpected, itemDropped2.name);
        Assert.AreEqual(itemDropped2AmountExpected, itemDropped2.quantity);
        Assert.AreEqual(itemDropped3NameExpected, itemDropped3.name);
        Assert.AreEqual(itemDropped3AmountExpected, itemDropped3.quantity);
        Assert.AreEqual(sporeItemsAmount, spore.inv.itemsAmount());
    }
}
