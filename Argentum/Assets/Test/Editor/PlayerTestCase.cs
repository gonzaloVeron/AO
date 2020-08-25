using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using JetBrains.Annotations;

public class PlayerTestCase
{
    private Player spore;

    private Player other;

    private Classification sporeClas;

    private Classification otherClas;

    private Attributes attributesSpore;

    private Attributes attributesOther;

    private Skills skillsSpore;

    private Skills skillsOther;

    private ListUtils utils;

    private Animal lobo;

    private Weapon largeSword;

    private Shield brokenShield;

    private DirectDamage damageSpell;

    [SetUp]
    public void SetUp()
    {
        utils = new ListUtils();
        attributesSpore = new Attributes(40, 38, 21, 21, 21, 21, 21);
        attributesOther = new Attributes(40, 38, 21, 21, 21, 21, 21);
        skillsSpore = new Skills(100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100);
        skillsOther = new Skills(100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100);
        sporeClas = new Warrior();
        otherClas = new Warrior();

        spore = new Player("Spore", attributesSpore, skillsSpore, sporeClas);
        other = new Player("Other", attributesOther, skillsOther, otherClas);

        largeSword = new MeleeWeapon("Espada larga", 4, 8, 0, 0, 1, 0.4f);
        brokenShield = new Shield("Escudo de madera roto", 1, 2, 0, 0, 1, 0f);

        spore.inv.goldCoins = 500;
        spore.inv.silverCoins = 500;
        spore.inv.copperCoins = 500;

        damageSpell = new DirectDamage("Tormenta de fuego", 55, 88, 750);

        var loboDrop = new List<Item>();
        var loboHitPoints = new Tuple<int, int>(2, 10);
        lobo = new Animal("Lobo", 75, loboHitPoints, 8, 80, 25, 100, loboDrop, 270f);
    }

    [Test]
    public void TameAnimalTest()
    {
        spore.clasf = new Druid();

        try
        {
            spore.TameAnimal(lobo);

            Assert.IsTrue(spore.tamedAnimals.contains(lobo));
        }
        catch (TheAnimalWasNotTamedException e)
        {
            Assert.Catch<TheAnimalWasNotTamedException>(() => throw e);
        }
    }

    [Test]
    public void AttackSuccessTest()
    {
        spore.weapon = largeSword;

        /*
         * Spore empieza con 22 de vida
         * daño: min/max (58, 74)
         * probabilidad de acierto: 10
         * aim: 85.6f
         * evasion sin escudo: 215.15f
         * evacion con escudo: 265.15f
         * 
         */

        var lifePointsExpected = new Range(0, 0).calculateRange();

        try
        {
            spore.Attack(other);

            Assert.IsTrue(lifePointsExpected.Contains(other.state.lifePoints));
            Assert.IsTrue(other.state.lifePoints == 0);
        }
        catch (FailedAttackException e)
        {
            Assert.Catch<FailedAttackException>(() => throw e);
            Assert.IsTrue(other.state.lifePoints == 22);
        }

    }

    [Test]
    public void SuccessProbabilityTest()
    {
        var probExpected = 10;
        Assert.AreEqual(probExpected, spore.successProbability(other));
    }

    [Test]
    public void AimTest()
    {
        var aimExpected = 85.6f;
        Assert.AreEqual(aimExpected, spore.aim());
    }

    [Test]
    public void EvasionWithoutShieldTest()
    {
        var evasionExpected = 215.15f;
        Assert.AreEqual(evasionExpected, spore.evasion(), 0.1d);
    }

    [Test]
    public void EvasionWithShieldTest()
    {
        spore.shield = brokenShield;
        var evasionExpected = 265.15f;
        Assert.AreEqual(evasionExpected, spore.evasion(), 0.1d);
    }

    [Test]
    public void ShieldingWithoutShieldTest()
    {
        var shieldingExpected = 0f;
        Assert.AreEqual(shieldingExpected, spore.shielding());
    }

    [Test]
    public void ShieldingWithShieldTest()
    {
        spore.shield = brokenShield;
        var shieldingExpected = 50f;
        Assert.AreEqual(shieldingExpected, spore.shielding());
    }

    [Test]
    public void HasAShieldTest()
    {
        Assert.IsFalse(spore.hasAShield());
    }

    [Test]
    public void AimModificatorTest()
    {
        var aimModExpected = 0.4f;
        Assert.AreEqual(aimModExpected, spore.aimModificator(spore.weapon));
    }

    [Test]
    public void DamageModificatorTest()
    {
        var damageModExpected = 0.9f;
        Assert.AreEqual(damageModExpected, spore.damageModificator(spore.weapon));
    }

    [Test]
    public void SkillAmountModificatorTest()
    {
        var skillAmount_1 = 15;
        var skillAmount_2 = 36;
        var skillAmount_3 = 75;
        var skillAmount_4 = 92;

        var skillAmountExpected_1 = 0;
        var skillAmountExpected_2 = 1;
        var skillAmountExpected_3 = 2;
        var skillAmountExpected_4 = 3;

        Assert.AreEqual(skillAmountExpected_1, spore.skillAmountModificator(skillAmount_1));
        Assert.AreEqual(skillAmountExpected_2, spore.skillAmountModificator(skillAmount_2));
        Assert.AreEqual(skillAmountExpected_3, spore.skillAmountModificator(skillAmount_3));
        Assert.AreEqual(skillAmountExpected_4, spore.skillAmountModificator(skillAmount_4));
    }

    [Test]
    public void CastSpellTest()
    {
        spore.clasf = new Wizard();
        spore.state.manaPoints = 1000;
        spore.state.maxManaPoints = 1000;

        spore.castSpell(damageSpell, other);

        Assert.AreEqual(250, spore.state.manaPoints);
        Assert.AreEqual(0, other.state.lifePoints);
    }

    [Test]
    public void BeingAttackedTest()
    {
        var lifeExpected = 1;
        spore.BeingAttacked(21);

        Assert.AreEqual(lifeExpected, spore.state.lifePoints);
    }

    [Test]
    public void PhysicalDefenseTest()
    {
        var defenseExpected = 0;

        Assert.AreEqual(defenseExpected, spore.physicalDefense());
    }

    [Test]
    public void MinArmorTest()
    {
        var armorExpected = 0;
        Assert.AreEqual(armorExpected, spore.minArmor());
    }

    [Test]
    public void MaxArmorTest()
    {
        var armorExpected = 0;
        Assert.AreEqual(armorExpected, spore.maxArmor());
    }

    [Test]
    public void MinHelmetTest()
    {
        var helmetExpected = 0;
        Assert.AreEqual(helmetExpected, spore.minHelmet());
    }

    [Test]
    public void MaxHelmetTest()
    {
        var helmetExpected = 0;
        Assert.AreEqual(helmetExpected, spore.maxHelmet());
    }

    [Test]
    public void MinShieldTest()
    {
        var shieldExpected = 0;
        Assert.AreEqual(shieldExpected, spore.minShield());
    }

    [Test]
    public void MaxShieldTest()
    {
        var shieldExpected = 0;
        Assert.AreEqual(shieldExpected, spore.maxShield());
    }

    [Test]
    public void BeAttackedWithMagicTest()
    {
        spore.BeAttackedWithMagic(30);
        var lifeExpected = 2;
        Assert.AreEqual(lifeExpected, spore.state.lifePoints);
    }

    [Test]
    public void SpellDamageTest()
    {
        var damageExpected = new Range(52, 105).calculateRange();
        Assert.IsTrue(damageExpected.Contains(spore.spellDamage(50, 100)));
    }

    [Test]
    public void PhysicalDamageTest()
    {
        spore.weapon = largeSword;
        var damageExpected = 58;
        Assert.AreEqual(damageExpected, spore.physicalDamage(largeSword.weapon.item1, spore.hitPoints.item1, spore.clasf.meleeDamageMod()));
    }

    [Test]
    public void MinArrowTest()
    {
        var minArrowExpected = 0;
        Assert.AreEqual(minArrowExpected, spore.minArrow());
    }

    [Test]
    public void MaxArrowTest()
    {
        var maxArrowExpected = 0;
        Assert.AreEqual(maxArrowExpected, spore.maxArrow());
    }

    [Test]
    public void TakeItemTest()
    {
        var itemAmountExpected = 1;
        spore.TakeItem(largeSword);
        Assert.AreEqual(itemAmountExpected, spore.inv.inv.Count);
    }

    [Test]
    public void DropGoldCointTest()
    {
        var amountExpected = 50;
        GoldCoin coins = spore.dropGoldCoins(amountExpected);

        Assert.AreEqual(amountExpected, coins.quantity);
    }

    [Test]
    public void DropSilverCointTest()
    {
        var amountExpected = 50;
        SilverCoin coins = spore.dropSilverCoins(amountExpected);

        Assert.AreEqual(amountExpected, coins.quantity);
    }

    [Test]
    public void DropCopperCointTest()
    {
        var amountExpected = 50;
        CopperCoin coins = spore.dropCopperCoins(amountExpected);

        Assert.AreEqual(amountExpected, coins.quantity);
    }

    [Test]
    public void GetGoldCoinsTest()
    {
        var coinsExpected = 500;
        Assert.AreEqual(coinsExpected, spore.getGoldCoins());
    }

    [Test]
    public void GetSilverCoinsTest()
    {
        var coinsExpected = 500;
        Assert.AreEqual(coinsExpected, spore.getSilverCoins());
    }

    [Test]
    public void GetCopperCoinsTest()
    {
        var coinsExpected = 500;
        Assert.AreEqual(coinsExpected, spore.getCopperCoins());
    }

    [Test]
    public void LearnSpellTest()
    {
        var spellsAmountExpected = 1;
        spore.LearnSpell(damageSpell);
        Assert.AreEqual(spellsAmountExpected, spore.spells.Count());
    }

    [Test]
    public void GainExperienceTest()
    {
        var expExpected = 50;
        spore.GainExperience(expExpected);
        Assert.AreEqual(expExpected, spore.xp);
    }

    [Test]
    public void NeedLvlUpFalseTest()
    {
        spore.xp = 99;
        Assert.IsFalse(spore.needLevelUp(50));
    }

    [Test]
    public void NeedLvlUpTrueTest()
    {
        spore.xp = 100;
        Assert.IsTrue(spore.needLevelUp(50));
    }

    [Test]
    public void LevelUpTest()
    {
        var maxLifePointsExpected = new Range(30, 33).calculateRange();
        var maxManaPointsExpected = 0;
        var maxEnergyPointsExpected = 55;
        var minHitPointsExpected = 4;
        var maxHitPointsExpected = 5;
        var lvlExpected = 2;
        var maxExpExpected = 300; //va a cambiar en el futuro
        var expExpect = 0;
        
        spore.LevelUp();

        Assert.IsTrue(maxLifePointsExpected.Contains(spore.state.maxLifePoints));
        Assert.AreEqual(maxManaPointsExpected, spore.state.maxManaPoints);
        Assert.AreEqual(maxEnergyPointsExpected, spore.state.maxEnergyPoints);
        Assert.AreEqual(minHitPointsExpected, spore.hitPoints.item1);
        Assert.AreEqual(maxHitPointsExpected, spore.hitPoints.item2);
        Assert.AreEqual(lvlExpected, spore.lvl);
        Assert.AreEqual(maxExpExpected, spore.xpMax);
        Assert.AreEqual(expExpect, spore.xp);
    }

    [Test]
    public void ChangeFaction()
    {
        var colorStringExpected = "FF1800";
        spore.ChangeFaction(new Chaos());

        Assert.AreEqual(colorStringExpected, spore.faction.color);
    }

    [Test]
    public void UseItemTest()
    {
        spore.inv.inv.Add(largeSword);
        spore.UseItem("Espada larga");
        Assert.AreEqual(largeSword, spore.weapon);
    }

    [Test]
    public void DropItemTest()
    {
        var expectedAmountItems = 0;
        spore.inv.inv.Add(brokenShield);
        Item dropedShield = spore.dropItem("Escudo de madera roto", 1);
        Assert.AreEqual(dropedShield.name, brokenShield.name);
        Assert.AreEqual(expectedAmountItems, spore.inv.inv.Count);
    }

    [Test]
    public void HasAmmunitionTest()
    {
        Assert.IsFalse(spore.hasAmmunition());
    }

    [Test]
    public void InitialLifeTest()
    {
        var lifePointsExpected = 22;
        Assert.AreEqual(lifePointsExpected, spore.initialLife());
    }

    [Test]
    public void DiscardAmmunition()
    {
        var arrowsAmountExpected = 54;
        Arrow flechas = new Arrow("Flecha simple", 1, 3, 55, 1.5f);
        spore.inv.inv.Add(flechas);
        spore.arrow = flechas;
        spore.DiscardAmmunition();

        Assert.IsTrue(spore.hasAmmunition());
        Assert.AreEqual(arrowsAmountExpected, spore.arrow.quantity);
    }

    [Test]
    public void IncrementHitPointsTest()
    {
        var minHitPointsExpected = 4;
        var maxHitPointsExpected = 5;
        spore.IncrementHitPoints();

        Assert.AreEqual(minHitPointsExpected, spore.hitPoints.item1);
        Assert.AreEqual(maxHitPointsExpected, spore.hitPoints.item2);
    }

    [Test]
    public void StealTest() //Las clases que no sean ladron roban un 3% del total del oro de la victima
    {
        var goldValuesExpected = new List<int>() { 500, 501 };
        var silverValuesExpected = new List<int>() { 500 , 509 };
        var copperValuesExpected = new List<int>() { 500 , 575 };

        other.inv.goldCoins = 10;
        other.inv.silverCoins = 300;
        other.inv.copperCoins = 500;

        spore.Steal(other);

        Assert.IsTrue(goldValuesExpected.Contains(spore.getGoldCoins()));
        Assert.IsTrue(silverValuesExpected.Contains(spore.getSilverCoins()));
        Assert.IsTrue(copperValuesExpected.Contains(spore.getCopperCoins()));
    }

    [Test]
    public void StealWithThiefTest()
    {
        spore.clasf = new Thief();
        var itemsNamesExpected = new List<string>() { "Red potion", "Dragon armor", "Bottle of water" };
        var goldValuesExpected = new List<int>() { 500, 505 };
        var silverValuesExpected = new List<int>() { 500, 545 };
        var copperValuesExpected = new List<int>() { 500, 875 };

        Potion redPotion = new Potion("Red potion", 30, 0, 30, 0.5f);
        Armor dragonArmor = new Armor("Dragon armor", 45, 50, 0, 0, 1, 5.6f);
        Consumable bottleOfWater = new Consumable("Bottle of water", 00, 0, 0, 0, 10, 30, 0.5f);

        other.inv.AddItem(redPotion);
        other.inv.AddItem(dragonArmor);
        other.inv.AddItem(bottleOfWater);
        other.inv.goldCoins = 10;
        other.inv.silverCoins = 300;
        other.inv.copperCoins = 500;

        spore.Steal(other);

        Assert.IsTrue(goldValuesExpected.Contains(spore.getGoldCoins()) && silverValuesExpected.Contains(spore.getSilverCoins()) && copperValuesExpected.Contains(spore.getCopperCoins()) || itemsNamesExpected.Contains(spore.inv.inv[0].name));
    }

    [Test]
    public void ExamineLifePointsOfTestCase1()
    {
        spore.skills.survival = 7;
        var stringExpected = "Dudoso";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase2()
    {
        spore.skills.survival = 13;
        var stringExpected = "Sano";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase3()
    {
        spore.skills.survival = 13;
        other.state.lifePoints = 10;
        var stringExpected = "Herido";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase4()
    {
        spore.skills.survival = 25;
        other.state.lifePoints = 18;
        var stringExpected = "Sano";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }


    [Test]
    public void ExamineLifePointsOfTestCase5()
    {
        spore.skills.survival = 25;
        other.state.lifePoints = 13;
        var stringExpected = "Herido";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase6()
    {
        spore.skills.survival = 25;
        other.state.lifePoints = 7;
        var stringExpected = "Gravemente Herido";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase7()
    {
        spore.skills.survival = 37;
        other.state.lifePoints = 20;
        var stringExpected = "Sano";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase8()
    {
        spore.skills.survival = 37;
        other.state.lifePoints = 15;
        var stringExpected = "Levemente Herido";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase9()
    {
        spore.skills.survival = 37;
        other.state.lifePoints = 7;
        var stringExpected = "Herido";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase10()
    {
        spore.skills.survival = 37;
        other.state.lifePoints = 4;
        var stringExpected = "Gravemente Herido";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase11()
    {
        spore.skills.survival = 51;
        var stringExpected = "Intacto";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase12()
    {
        spore.skills.survival = 51;
        other.state.lifePoints = 21;
        var stringExpected = "Sano";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase13()
    {
        spore.skills.survival = 51;
        other.state.lifePoints = 15;
        var stringExpected = "Levemente Herido";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase14()
    {
        spore.skills.survival = 51;
        other.state.lifePoints = 10;
        var stringExpected = "Herido";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase15()
    {
        spore.skills.survival = 51;
        other.state.lifePoints = 3;
        var stringExpected = "Gravemente Herido";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase16()
    {
        spore.skills.survival = 51;
        other.state.lifePoints = 1;
        var stringExpected = "Casi Muerto";
        Assert.AreEqual(stringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase17()
    {
        spore.skills.survival = 80;
        other.state.lifePoints = 15;
        var lifeToStringExpected = $"({other.state.lifePoints}/{other.state.maxLifePoints})";
        Assert.AreEqual(lifeToStringExpected, spore.examineLifePointsOf(other));
    }

    [Test]
    public void ResourcesObtainedTest()
    {
        spore.clasf = new WorkingMan();
        spore.lvl = 17;

        var numRange = new Range(1, 5).calculateRange();

        Assert.IsTrue(numRange.Contains(spore.resourcesObtained()));
    }

    [Test]
    public void ExtractionChanceTest()
    {
        spore.skills.fishing = 67;

        Assert.AreEqual(23, spore.extractionChance(spore.skills.fishing));
    }

    [Test]
    public void SubstractResourcesTestCatchFish()
    {
        Shoal shoal = new Shoal(500);
        var rod = new FishingRod(1, 0.5f);
        spore.clasf = new WorkingMan();
        spore.skills.fishing = 100;
        spore.lvl = 50;
        spore.tool = rod;

        var totalResourcesExpected = new Range(492, 499).calculateRange();

        spore.ExtractResource(shoal);

        Assert.IsTrue(spore.inv.existsItem("Cornalito"));
        Assert.IsTrue(totalResourcesExpected.Contains(shoal.resourceAmount));
    }

    [Test]
    public void SubstractResourcesTestCutdown()
    {
        Treee tree = new Treee(500);
        var axe = new Axe(1, 5f);
        spore.clasf = new WorkingMan();
        spore.skills.cutDownTrees = 100;
        spore.lvl = 50;
        spore.tool = axe;

        var totalResourcesExpected = new Range(492, 499).calculateRange();

        spore.ExtractResource(tree);

        Assert.IsTrue(spore.inv.existsItem("Madera"));
        Assert.IsTrue(totalResourcesExpected.Contains(tree.resourceAmount));
    }
    [Test]
    public void SubstractResourcesTestExtractRoots()
    {
        Bush bush = new Bush(500);
        var scissors = new Scissors(1, 5f);
        spore.clasf = new WorkingMan();
        spore.skills.botany = 100;
        spore.lvl = 50;
        spore.tool = scissors;

        var totalResourcesExpected = new Range(492, 499).calculateRange();

        spore.ExtractResource(bush);

        Assert.IsTrue(spore.inv.existsItem("Raiz"));
        Assert.IsTrue(totalResourcesExpected.Contains(bush.resourceAmount));
    }

}