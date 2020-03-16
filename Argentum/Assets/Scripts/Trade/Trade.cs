using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Trade
{
    private Player seller;

    private Player buyer;

    private List<Item> sellerOffer;

    private int sellerGoldOffer;

    private List<Item> buyerOffer;

    private int buyerGoldOffer;

    private bool sellerIsReady;

    private bool buyerIsReady;

    public Trade(Player seller, Player buyer)
    {
        this.seller = seller;
        this.buyer = buyer;
        this.sellerOffer = new List<Item>();
        this.buyerOffer = new List<Item>();
        this.sellerGoldOffer = 0;
        this.buyerGoldOffer = 0;
        this.sellerIsReady = false;
        this.buyerIsReady = false;
    }

    public void AddToOffer(Player who, Item i)
    {
        this.template(who, () => this.sellerOffer.Add(i.copy()), () => this.buyerOffer.Add(i.copy()));
    }
    public void RemoveOffer(Player who, string itemName, int amount)
    {
        this.template(who, () => this.removeByQuantity(who, this.sellerOffer, itemName, amount), () => this.removeByQuantity(who, this.buyerOffer, itemName, amount));
    }
    public void AddGold(Player who, int amount)
    {
        this.template(who, () => this.sellerGoldOffer += amount, () => this.buyerGoldOffer += amount);
    }
    public void RemoveGold(Player who, int amount)
    {
        this.template(who, () => this.sellerGoldOffer = Mathf.Max(0, this.sellerGoldOffer - amount), () => this.buyerGoldOffer = Mathf.Max(0, this.buyerGoldOffer - amount));
    }
    public void Confirm(Player who)
    {
        this.template(who, () => this.sellerIsReady = true, () => this.buyerIsReady = true);
    }

    public void Terminate()
    {
        buyerOffer.ForEach(i => this.seller.TakeItem(i));
        seller.gold = Mathf.Max(0, this.seller.gold - sellerGoldOffer);
        seller.gold += buyerGoldOffer;

        sellerOffer.ForEach(i => this.buyer.TakeItem(i));
        buyer.gold = Mathf.Max(0, this.buyer.gold - buyerGoldOffer);
        buyer.gold += sellerGoldOffer;
        
        sellerOffer.ForEach(i => this.seller.inv.RemoveItemByQuantity(i.name, i.quantity));
        buyerOffer.ForEach(i => this.buyer.inv.RemoveItemByQuantity(i.name, i.quantity));

        this.Cancel();
    }

    public void Cancel()
    {
        this.seller = null;
        this.buyer = null;
        this.sellerOffer = null;
        this.buyerOffer = null;
        this.sellerGoldOffer = 0;
        this.buyerGoldOffer = 0;
        this.sellerIsReady = false;
        this.buyerIsReady = false;
    }

    public bool ready() => this.buyerIsReady && this.sellerIsReady;

    //----
    public List<Item> getOfferOf(Player who) => this.templateFun(who, () => this.sellerOffer, () => this.buyerOffer);

    public int getGoldOfferOf(Player who) => this.templateFun(who, () => this.sellerGoldOffer, () => this.buyerGoldOffer);

    public bool offerConfirmed(Player who) => this.templateFun(who, () => this.sellerIsReady, () => this.buyerIsReady);

    public Player getSeller() => this.seller;
    public Player getBuyer() => this.buyer;

    private void template(Player who, Action sellerAction, Action buyerAction)
    {
        if (who == seller)
        {
            this.sellerIsReady = false;
            sellerAction.Invoke();
        }
        else
        {
            this.buyerIsReady = false;
            buyerAction.Invoke();
        }
    }

    private T templateFun<T>(Player who, Func<T> sellerFun, Func<T> buyerFunc)
    {
        if (who == seller)
        {
            return sellerFun.Invoke();
        }
        else
        {
            return buyerFunc.Invoke();
        }
    }

    private void removeByQuantity(Player who, List<Item> items, string itemName, int amount)
    {
        var itemFinded = items.Find(i => i.name == itemName);
        if (itemFinded.quantity - amount <= 0)
        {
            items.Remove(itemFinded);
        }
        else
        {
            itemFinded.quantity -= amount;
        }
    }
}