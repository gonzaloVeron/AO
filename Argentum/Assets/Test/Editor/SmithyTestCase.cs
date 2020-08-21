using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
public class SmithyTestCase
{
    private Smithy smithy;
   
    private Player spore;

    private Skills skillsSpore;

    private Attributes attributesSpore;

    private RecipeService recipeService;

    private ItemEquipableService iEquipableService;

    [SetUp]
    public void SetUp()
    {
        attributesSpore = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        spore = new Player("Spore", attributesSpore, skillsSpore, new Wizard());
        smithy = new Smithy(spore);

        recipeService = new RecipeService();
        iEquipableService = new ItemEquipableService();

        Recipe r1 = new Recipe("Armadura completa murex", 70, "Smithy");
        r1.AddElement("Lingote de hierro", 300);
        r1.AddElement("Lingote de plata", 100);
        r1.AddElement("Armadura de placas completa", 1);

        recipeService.Save(r1);

        Armor murex = new Armor("Armadura completa murex", 30, 35, 0, 0, 1, 3f);

        iEquipableService.Save(murex);


        var armor = new Armor("Armadura de placas completa", 20, 25, 0, 0, 1, 2f);
        var iroIngot = new Resource("Lingote de hierro", 500, 0f);
        var silIngot = new Resource("Lingote de plata", 150, 0f);

        spore.TakeItem(armor);
        spore.TakeItem(iroIngot);
        spore.TakeItem(silIngot);
        spore.skills.smithy = 100;
    }
    
    [TearDown]
    public void TearDown()
    {
        recipeService.DropCollection();
        iEquipableService.DropCollection();
    }

    [Test]
    public void CraftItemTest()
    {
        smithy.CraftItem("Armadura completa murex");

        Assert.AreEqual(1, spore.inv.fetchItem("Armadura completa murex").quantity);
        Assert.AreEqual(200, spore.inv.fetchItem("Lingote de hierro").quantity);
        Assert.AreEqual(50, spore.inv.fetchItem("Lingote de plata").quantity);
        Assert.IsTrue(!spore.inv.existsItem("Armadura de placas completa"));
    }

    [Test]
    public void recipesAvailableTest()
    {
        spore.skills.smithy = 50;

        Assert.IsTrue(smithy.recipesAvailable(spore.skills.smithy).Count == 0);
    }

}
