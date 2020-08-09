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

    private RecipeService recipeService;

    private ItemEquipableService iEquipableService;

    [SetUp]
    public void SetUp()
    {
        attributesSpore = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        spore = new Player("Spore", attributesSpore, skillsSpore, new Warrior());
        carpentry = new Carpentry(spore);

        recipeService = new RecipeService();
        iEquipableService = new ItemEquipableService();

        Recipe r1 = new Recipe("Arco compuesto", 30, "Carpentry");
        r1.AddElement("Madera", 450);
        recipeService.Save(r1);

        RangedWeapon arco = new RangedWeapon("Arco compuesto", 15, 25, 1, 0.7f);
        iEquipableService.Save(arco);

        var troncos = new Resource("Madera", 2000, 0f);

        spore.skills.carpentry = 100;
        spore.TakeItem(troncos);
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
