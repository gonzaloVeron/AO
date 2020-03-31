using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
public class AlchemyTestCase
{
    private Alchemy alchemy;

    private ListUtils utils;

    private Attributes attributesSpore;

    private Skills skillsSpore;

    private Player spore;

    [SetUp]
    public void SetUp()
    {
        utils = new ListUtils();
        attributesSpore = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        spore = new Player("Spore", attributesSpore, skillsSpore, new Warrior());

        alchemy = new Alchemy(spore);

        var raices = new Resource("Raiz", 150, 0f);
        spore.skills.alchemy = 1;
        spore.TakeItem(raices);
    }

    [Test]
    public void GeneratePotionTest()
    {
        var expectedPotionAmount = 1;

        alchemy.GeneratePotion("Pocion de regeneracion");

        Assert.IsTrue(spore.inv.existsItem("Pocion de regeneracion"));
        Assert.AreEqual(expectedPotionAmount, spore.inv.fetchItem("Pocion de regeneracion").quantity);
    }
    [Test]
    public void RecipesAvailableTest()
    {
        var expectedElements = new List<string> { "Pocion de regeneracion" };

        Assert.AreEqual(expectedElements, alchemy.recipesAvailable(spore.skills.alchemy));
    }
    [Test]
    public void RemoveItemsFromRecipeTest()
    {
        var expectedAmount = 145;

        alchemy.RemoveItemsFromRecipe(spore, alchemy.potionRecipes.Find(i => i.name == "Pocion de regeneracion").itemsNeeded);

        Assert.AreEqual(expectedAmount, spore.inv.fetchItem("Raiz").quantity);
    }

    [Test]
    public void FindItemsNeededTest()
    {
        var expectedElements = new List<Tuple<string, int>> { new Tuple<string, int>("Raiz", 5) };

        Assert.IsTrue(expectedElements.TrueForAll(t => alchemy.findItemsNeeded("Pocion de regeneracion").Exists(tu => tu.item1 == t.item1)));
    }

}
