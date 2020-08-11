using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework;

public class ForgeTestCase
{
    private Forge forge;

    private Attributes attributesSpore;

    private Skills skillsSpore;

    private Player spore;

    private ResourceService resourceService;

    private RecipeService recipeService;

    [SetUp]
    public void SetUp()
    {
        attributesSpore = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        spore = new Player("Spore", attributesSpore, skillsSpore, new Warrior());
        forge = new Forge(spore);
        resourceService = new ResourceService();
        recipeService = new RecipeService();


        Resource res1 = new Resource("Mineral de plata", 1, 0.3f);
        Resource res2 = new Resource("Lingote de plata", 1, 0.1f);

        Recipe rec1 = new Recipe("Lingote de plata", 50, "Forge");
        rec1.AddElement(res1.name, 20);

        resourceService.Save(res1);
        resourceService.Save(res2);

        recipeService.Save(rec1);
    }

    [TearDown]
    public void TearDown()
    {
        resourceService.DropCollection();
        recipeService.DropCollection();
    }

    [Test]
    public void GenerateIngotTest()
    {
        var mineralesDePlata = new Resource("Mineral de plata", 100, 0f);
        spore.skills.mining = 100;

        spore.TakeItem(mineralesDePlata);

        forge.CraftItem("Lingote de plata");
        forge.CraftItem("Lingote de plata");
        forge.CraftItem("Lingote de plata");

        Assert.AreEqual(40, spore.inv.fetchItem("Mineral de plata").quantity);
        Assert.IsTrue(spore.inv.existsItem("Lingote de plata"));
        Assert.AreEqual(3, spore.inv.fetchItem("Lingote de plata").quantity);
    }

}
