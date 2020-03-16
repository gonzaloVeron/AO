using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TradeTestCase
{
    private Trade trade;

    private Player spore;

    private Player necroz;

    private MeleeWeapon item1;

    private MeleeWeapon item2;

    private MeleeWeapon item3;

    private MeleeWeapon item4;

    private Consumable item5;

    private Consumable item6;

    private Consumable item7;

    private Consumable item8;

    private Consumable item9;

    private Consumable item10;

    private Consumable item11;

    private Consumable item12;

    private Armor item13;

    private Helmet item14;

    private Magical item15;

    private Consumable item16;

    private Attributes attr;

    private Skills skill;

    [SetUp]
    public void SetUp()
    {
        attr = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skill = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        this.spore = new Player("Spore", attr, skill, new Warrior());
        this.necroz = new Player("Spore", attr, skill, new Wizard());
        this.trade = new Trade(spore, necroz);

        this.item1 = new MeleeWeapon("Silver sword", 10, 17, 0, 0, 5, 1.7f);
        this.item2 = new MeleeWeapon("Lazull staff", 17, 25, 0, 0, 2, 0.9f);
        this.item3 = new MeleeWeapon("Dagger +2", 3, 5, 0, 0, 1, 0.1f);
        this.item5 = new Consumable("Red potion", 30, 0, 0, 0, 0, 7841, 0f);
        this.item6 = new Consumable("Blue potion", 0, 0.4f, 0, 0, 0, 9794, 0f);
        this.item8 = new Consumable("Chicken", 0, 0, 0, 75, 0, 55, 0f);
        this.item10 = new Consumable("Water bottle", 0, 0, 0, 0, 20, 119, 0f);
        this.item13 = new Armor("Blue dragon armor", 45, 50, 0, 0, 2, 5.7f);
        this.item15 = new Magical("Spectral ring", 0, 15, 5, 0f);

        this.item4 = new MeleeWeapon("Silver sword", 10, 17, 0, 0, 9, 1.7f);
        this.item16 = new Consumable("Red potion", 30, 0, 0, 0, 0, 1795, 0f);
        this.item7 = new Consumable("Blue potion", 0, 0.4f, 0, 0, 0, 141, 0f);
        this.item9 = new Consumable("Goat cheese", 0, 0, 0, 25, 0, 17, 0f);
        this.item11 = new Consumable("Fruit juice", 0, 0, 0, 0, 100, 64, 0f);
        this.item12 = new Consumable("Experience scroll", 0, 0, 0, 0, 0, 1, 0f);
        this.item14 = new Helmet("Viking helmet", 30, 35, 0, 0, 1, 3f);

        spore.TakeItem(item1);
        spore.TakeItem(item2);
        spore.TakeItem(item3);
        spore.TakeItem(item5);
        spore.TakeItem(item6);
        spore.TakeItem(item8);
        spore.TakeItem(item10);
        spore.TakeItem(item13);
        spore.TakeItem(item15);
        spore.gold = 5000;

        necroz.TakeItem(item4);
        necroz.TakeItem(item16);
        necroz.TakeItem(item7);
        necroz.TakeItem(item9);
        necroz.TakeItem(item11);
        necroz.TakeItem(item12);
        necroz.TakeItem(item14);
        necroz.gold = 87841;

        //---
        trade.AddToOffer(spore, item2);
        trade.AddToOffer(spore, item13);
        trade.AddToOffer(spore, item15);

        trade.AddToOffer(necroz, item12);
        trade.AddToOffer(necroz, item14);

        trade.AddGold(necroz, 50000);
        trade.AddGold(spore, 5);

        trade.Confirm(spore);
        trade.Confirm(necroz);

    }

    [Test]
    public void AddToOfferTest()
    {
        Assert.AreEqual(3, trade.getOfferOf(spore).Count);
        Assert.AreEqual(2, trade.getOfferOf(necroz).Count);
    }

    [Test]
    public void AddGoldTest()
    {
        Assert.AreEqual(50000, trade.getGoldOfferOf(necroz));
        Assert.AreEqual(5, trade.getGoldOfferOf(spore));
    }

    [Test]
    public void RemoveOfferTest()
    {
        trade.RemoveOffer(spore, item15.name, 3);
        trade.RemoveOffer(necroz, item14.name, 1);

        Assert.AreEqual(1, trade.getOfferOf(necroz).Count);
        Assert.AreEqual(2, trade.getOfferOf(spore).Find(i => i.name == item15.name).quantity);
    }

    [Test]
    public void RemoveGoldTest()
    {
        trade.RemoveGold(spore, 3);
        trade.RemoveGold(necroz, 57);

        Assert.AreEqual(49943, trade.getGoldOfferOf(necroz));
        Assert.AreEqual(2, trade.getGoldOfferOf(spore));
    }

    [Test]
    public void ConfirmTest()
    {
        Assert.IsTrue(trade.offerConfirmed(spore));
        Assert.IsFalse(!trade.offerConfirmed(necroz));
    }

    [Test]
    public void CancelTest()
    {
        trade.Cancel();

        Assert.IsNull(trade.getSeller());
        Assert.IsNull(trade.getBuyer());
        Assert.IsNull(trade.getOfferOf(null));
        Assert.IsNull(trade.getOfferOf(necroz));
        Assert.AreEqual(0, trade.getGoldOfferOf(null));
        Assert.AreEqual(0, trade.getGoldOfferOf(necroz));
        Assert.IsFalse(trade.offerConfirmed(null));
        Assert.IsFalse(trade.offerConfirmed(necroz));
    }

    [Test]
    public void TerminateTest()
    {
        var utils = new ListUtils();

        trade.AddToOffer(spore, item6);
        trade.RemoveOffer(spore, "Blue potion", 4794);
        trade.Terminate();

        Assert.AreEqual(8, necroz.inv.inv.Count);
        Assert.AreEqual(8, spore.inv.inv.Count);

        Assert.IsTrue(necroz.inv.existsItem(item2.name));
        Assert.IsTrue(necroz.inv.existsItem(item13.name));
        Assert.IsTrue(necroz.inv.existsItem(item15.name));
        Assert.AreEqual(5141, necroz.inv.fetchItem("Blue potion").quantity);

        Assert.IsTrue(spore.inv.existsItem(item12.name));
        Assert.IsTrue(spore.inv.existsItem(item14.name));
        Assert.AreEqual(4794, spore.inv.fetchItem("Blue potion").quantity);

        Assert.AreEqual(54995, spore.gold);
        Assert.AreEqual(37846, necroz.gold);
    }

    [Test]
    public void ReadyTest()
    {
        Assert.IsTrue(trade.ready());
    }


}
