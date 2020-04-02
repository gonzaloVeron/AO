using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
public class SmithyTestCase
{
    private ListUtils utils;
    
    private Smithy smithy;
   
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
        smithy = new Smithy(spore);

        var armor = new Armor("Armadura de placas completas", 20, 25, 0, 0, 1, 2f);
        var iroIngot = new Resource("Lingote de hierro", 500, 0f);
        var silIngot = new Resource("Lingote de plata", 150, 0f);

        spore.TakeItem(armor);
        spore.TakeItem(iroIngot);
        spore.TakeItem(silIngot);
        spore.skills.smithy = 100;
    }

    [Test]
    public void CraftItemTest()
    {
        smithy.CraftItem("Armadura completa murex");

        Assert.AreEqual(1, spore.inv.fetchItem("Armadura completa murex").quantity);
        Assert.AreEqual(200, spore.inv.fetchItem("Lingote de hierro").quantity);
        Assert.AreEqual(50, spore.inv.fetchItem("Lingote de plata").quantity);
        Assert.IsTrue(!spore.inv.existsItem("Armadura de placas completas"));
    }

    [Test]
    public void recipesAvailableTest()
    {   //Para este entonces solo existe la "Armadura completa murex" como receta y pide 70 skill
        spore.skills.smithy = 50;

        Assert.IsTrue(smithy.recipesAvailable(spore.skills.smithy).Count == 0);
    }

}
