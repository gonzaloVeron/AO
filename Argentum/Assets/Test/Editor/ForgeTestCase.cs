using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework;

public class ForgeTestCase
{
    private Forge forge;

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
        forge = new Forge(spore);
    }

    [Test]
    public void GenerateIngotTest()
    {
        var mineralesDePlata = new Resource("Mineral de plata", 100, 0f);
        spore.skills.mining = 100;

        spore.TakeItem(mineralesDePlata);

        forge.GenerateIngot("Lingote de plata");
        forge.GenerateIngot("Lingote de plata");
        forge.GenerateIngot("Lingote de plata");

        Assert.AreEqual(40, spore.inv.fetchItem("Mineral de plata").quantity);
        Assert.IsTrue(spore.inv.existsItem("Lingote de plata"));
        Assert.AreEqual(3, spore.inv.fetchItem("Lingote de plata").quantity);
    }

}
