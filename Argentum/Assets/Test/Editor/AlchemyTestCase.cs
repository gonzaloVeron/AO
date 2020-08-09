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

    private RecipeService recipeService;

    private PotionService potionService;

    [SetUp]
    public void SetUp()
    {
        utils = new ListUtils();
        attributesSpore = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        spore = new Player("Spore", attributesSpore, skillsSpore, new Warrior());
        alchemy = new Alchemy(spore);

        recipeService = new RecipeService();
        potionService = new PotionService();

        Recipe r1 = new Recipe("Pocion roja", 5, "Alchemy");
        r1.AddElement("Raiz", 15);

        Recipe r2 = new Recipe("Pocion azul", 10, "Alchemy");
        r2.AddElement("Raiz", 25);

        recipeService.Save(r1);
        recipeService.Save(r2);

        Potion p1 = new Potion("Pocion roja", 30, 0, 1, 0f);
        Potion p2 = new Potion("Pocion azul", 0, 4, 1, 0f);
        potionService.Save(p1);
        potionService.Save(p2);

        var raices = new Resource("Raiz", 150, 0f);

        spore.skills.alchemy = 100;
        spore.TakeItem(raices);
    }

    [TearDown]
    public void TearDown()
    {
        recipeService.DropCollection();
        potionService.DropCollection();
    }

    [Test]
    public void GeneratePotionTest()
    {
        var expectedPotionAmount = 1;
        var expectedRoots = 135;

        alchemy.CraftItem("Pocion roja");

        Assert.IsTrue(spore.inv.existsItem("Pocion roja"));
        Assert.AreEqual(expectedRoots, spore.inv.fetchItem("Raiz").quantity);
        Assert.AreEqual(expectedPotionAmount, spore.inv.fetchItem("Pocion roja").quantity);
    }
    [Test]
    public void RecipesAvailableTest()
    {
        Assert.AreEqual(2, alchemy.recipesAvailable(spore.skills.alchemy).Count);
    }

}
