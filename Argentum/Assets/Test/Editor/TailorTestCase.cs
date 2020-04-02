using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TailorTestCase
{
    private ListUtils utils;

    private Tailor tailor;

    private Player spore;

    private Skills skillsSpore;

    private Attributes attributesSpore;

    [SetUp]
    public void SetUp()
    {
        utils = new ListUtils();
        attributesSpore = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        spore = new Player("Spore", attributesSpore, skillsSpore, new Wizard());
        tailor = new Tailor(spore);

        var pieles = new Resource("Piel de lobo", 10, 0f);

        spore.TakeItem(pieles);
        spore.skills.tailoring = 100;
    }

    [Test]
    public void CraftItemTest()
    {
        tailor.CraftItem("Tunica de mago");

        Assert.AreEqual(1, spore.inv.fetchItem("Tunica de mago").quantity);
        Assert.AreEqual(5, spore.inv.fetchItem("Piel de lobo").quantity);
    }
    [Test]
    public void RecipeAvailableTest() //Al momento de creado este test solo existia la tunica de mago
    {
        Assert.IsTrue(tailor.recipesAvailable(spore.skills.tailoring).Count == 1);
    }
}
