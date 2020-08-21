using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class TailorTestCase
{
    private Tailor tailor;

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
        tailor = new Tailor(spore);

        recipeService = new RecipeService();
        iEquipableService = new ItemEquipableService();

        Recipe r1 = new Recipe("Tunica de mago", 15, "Tailor");
        r1.AddElement("Piel de lobo", 5);
        recipeService.Save(r1);

        Armor tuni = new Armor("Tunica de mago", 1, 5, 5, 0, 1, 0.1f);
        iEquipableService.Save(tuni);

        var pieles = new Resource("Piel de lobo", 10, 0f);

        spore.TakeItem(pieles);
        spore.skills.tailoring = 100;
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
