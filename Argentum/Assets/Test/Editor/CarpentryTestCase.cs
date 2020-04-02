using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
public class CarpentryTestCase
{
    private Carpentry carpentry;

    private Player spore;

    private Skills skillsSpore;

    private Attributes attributesSpore;

    private ListUtils utils;

    [SetUp]
    public void SetUp()
    {
        utils = new ListUtils();
        attributesSpore = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        spore = new Player("Spore", attributesSpore, skillsSpore, new Warrior());

        carpentry = new Carpentry(spore);

        spore.skills.carpentry = 100;
        spore.TakeItem(new Resource("Madera", 2000, 0f));
    }

    [Test]
    public void CraftItemTest()
    {
        carpentry.CraftItem("Arco compuesto");

        Assert.AreEqual(1550, spore.inv.fetchItem("Madera").quantity);
        Assert.AreEqual(1, spore.inv.fetchItem("Arco compuesto").quantity);
    }

    [Test]
    public void RecipesAvailableTest() //Al momento de ejecucion de este test solo existe "Arco compuesto"
    {
        Assert.AreEqual(1, carpentry.recipesAvailable(spore.skills.carpentry).Count);
    }

}
