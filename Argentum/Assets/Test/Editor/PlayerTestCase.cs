using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class PlayerTestCase
{
    private Player spore;

    private Player other;

    private Attributes attributesSpore;

    private Attributes attributesOther;

    private Skills skillsSpore;

    private Skills skillsOther;

    private ListUtils utils;

    [SetUp]
    public void SetUp()
    {
        utils = new ListUtils();
        attributesSpore = new Attributes(40, 38, 0, 0, 0, 0, 0);
        attributesOther = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        skillsOther = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        spore = new Player("Spore", attributesSpore, skillsSpore, new Warrior());
        other = new Player("Other", attributesOther, skillsOther, new Warrior());
        other.armor = new Armor("Vestimenta Simple", 3, 6, 0, 0, 1, 0f);
        other.helmet = new Helmet("Capucha", 2, 5, 0, 0, 1, 0f);
        other.shield = new Shield("Escudo de madera roto", 1, 2, 0, 0, 1, 0f);
        other.weapon = new MeleeWeapon("Daga rota", 1, 1, 0, 0, 1, 0f);
        other.state.lifePoints = 300;
        other.state.maxLifePoints = 300;
    }

    [Test]
    public void WarriorAttackCreatureTest() //Controlar el porcentaje de probabilidad de acierto
    {
        var sword = new MeleeWeapon("Espada larga", 4, 8, 0, 0, 1, 0.4f);
        spore.lvl = 30;
        spore.skills.armedCombat = 11;
        spore.hitPoints.item1 = 1;
        spore.hitPoints.item2 = 1;
        spore.TakeItem(sword);
        spore.UseItem("Espada larga");

        var mostroDrop = new List<Item>();
        var mostroHitPoints = new Tuple<int, int>(2, 10);
        Creature mostro = new Creature("Anfisbena", 75, mostroHitPoints, 8, 80, 25, 100, mostroDrop); //Tiene las stats de un lobo

        var monsterLife = new Range(11, 25).calculateRange();
        try
        {
            spore.Attack(mostro);

            Debug.Log("Vida del mostro: " + mostro.state.lifePoints);
            Assert.IsTrue(monsterLife.Contains(mostro.state.lifePoints));
        }
        catch (FailedAttackException e)
        {
            Assert.Catch<FailedAttackException>(() => throw e);
            Assert.IsFalse(monsterLife.Contains(mostro.state.lifePoints));
        }
    }

    [Test]
    public void TameAnimalTest()
    {
        try
        {
            spore.clasf = new Druid();
            spore.lvl = 30;
            spore.attributes.charisma = 22;
            spore.skills.tameAnimals = 100;
            spore.hitPoints.item1 = 1;
            spore.hitPoints.item2 = 1;

            var loboDrop = new List<Item>();
            var loboHitPoints = new Tuple<int, int>(2, 10);
            Animal lobo = new Animal("Lobo", 75, loboHitPoints, 8, 80, 25, 100, loboDrop, 270f);

            spore.TameAnimal(lobo);

            Assert.IsTrue(spore.tamedAnimals.contains(lobo));
        }
        catch (TheAnimalWasNotTamedException e)
        {
            Assert.Catch<TheAnimalWasNotTamedException>(() => throw e);
        }
    }

    [Test]
    public void TameAnimalMaxTest() //Deberia crear una exception y verificar antes si hay espacio y catchear un error mas especifico
    {
        Assert.Catch<IndexOutOfRangeException>(() =>
        {
            spore.clasf = new Druid();
            spore.lvl = 30;
            spore.attributes.charisma = 22;
            spore.skills.tameAnimals = 100;
            spore.hitPoints.item1 = 1;
            spore.hitPoints.item2 = 1;

            var loboDrop = new List<Item>();
            var loboHitPoints = new Tuple<int, int>(2, 10);
            Animal lobo = new Animal("Lobo", 75, loboHitPoints, 8, 80, 25, 100, loboDrop, 270f);
            Animal lobo1 = new Animal("Lobo", 75, loboHitPoints, 8, 80, 25, 100, loboDrop, 270f);
            Animal lobo2 = new Animal("Lobo", 75, loboHitPoints, 8, 80, 25, 100, loboDrop, 270f);
            Animal lobo3 = new Animal("Lobo", 75, loboHitPoints, 8, 80, 25, 100, loboDrop, 270f);

            spore.TameAnimal(lobo);
            spore.TameAnimal(lobo1);
            spore.TameAnimal(lobo2);
            spore.TameAnimal(lobo3);
        });
    }

    [Test]
    public void TameCreatureTest()
    {
        Assert.Catch<CantTameCreaturesException>(() =>
        {
            spore.clasf = new Druid();
            spore.lvl = 30;
            spore.attributes.charisma = 22;
            spore.skills.tameAnimals = 100;
            spore.hitPoints.item1 = 1;
            spore.hitPoints.item2 = 1;

            var mostroDrop = new List<Item>();
            var mostroHitPoints = new Tuple<int, int>(2, 10);
            Creature mostro = new Creature("Anfisbena", 75, mostroHitPoints, 8, 80, 25, 100, mostroDrop);

            spore.TameAnimal(mostro);
        });
    }

    [Test]
    public void BardAttackClericTest()
    {
        try
        {
            var knuckles = new Knuckles("Nudillos de plata", 4, 9, 1, 0.6f);
            spore.clasf = new Bard();
            spore.lvl = 30;
            spore.skills.martialArts = 100;
            spore.hitPoints.item1 = 1;
            spore.hitPoints.item2 = 1;
            spore.TakeItem(knuckles);
            spore.UseItem("Nudillos de plata");

            other.clasf = new Cleric();
            other.lvl = 30;
            other.skills.shieldDefese = 100;
            other.skills.combatTactics = 100;

            spore.Attack(other);

            var lifePointsExpected = new Range(277, 290).calculateRange();

            Assert.IsTrue(lifePointsExpected.Contains(other.state.lifePoints));
        }
        catch (FailedAttackException e)
        {
            Assert.Catch<FailedAttackException>(() => throw e);
        }
    }

    [Test]
    public void AttackWithBowTest()
    {
        try
        {
            var bow = new RangedWeapon("Compoud bow", 4, 9, 1, 0.5f);
            var arrow = new Arrow("Arrow +3", 1, 6, 5, 0f);
            spore.clasf = new Druid();
            spore.lvl = 30;
            spore.skills.projectileWeapons = 100;
            spore.hitPoints.item1 = 1;
            spore.hitPoints.item2 = 1;
            spore.TakeItem(bow);
            spore.TakeItem(arrow);
            spore.UseItem("Compoud bow");
            spore.UseItem("Arrow +3");

            other.clasf = new Druid();
            other.lvl = 30;
            other.skills.combatTactics = 100;
            other.skills.shieldDefese = 100;
            other.state.lifePoints = 300;
            other.armor = null;
            other.helmet = null;
            other.shield = null;

            spore.Attack(other);

            var lifePointsExpected = new Range(232, 254).calculateRange();
            var arrowAmountExpected = 4;

            Assert.IsTrue(lifePointsExpected.Contains(other.state.lifePoints));
            Assert.AreEqual(arrowAmountExpected, spore.arrow.quantity);
        }
        catch (FailedAttackException e)
        {
            Assert.Catch<FailedAttackException>(() => throw e);
        }
    }

    [Test]
    public void AttackWithProbOfSuccess()
    {
        try
        {
            //Settear a spore para el test
            spore.clasf = new Paladin();
            spore.lvl = 30;
            spore.skills.armedCombat = 100;
            //Settear al atacado para el test
            other.clasf = new Druid();
            other.lvl = 30;
            other.attributes.agility = 38;
            other.skills.combatTactics = 100;
            other.skills.shieldDefese = 100;

            //-----//
            var espada = new MeleeWeapon("Espada larga rota", 1, 1, 0, 0, 1, 0.5f);
            spore.hitPoints.item1 = 1;
            spore.hitPoints.item2 = 1;
            spore.TakeItem(espada);
            spore.UseItem("Espada larga rota");

            spore.Attack(other);

            //el enemigo tiene una armadura de 6/13 por lo tanto el mejor golpe es 8 - 6 que resulta ser (300 - 2) = 298
            var lifePointsExpected = new Range(298, 300).calculateRange();
            var experienceExpected = 2;

            Assert.IsTrue(lifePointsExpected.Contains(other.state.lifePoints));
            Assert.AreEqual(experienceExpected, spore.xp);
        }
        catch (FailedAttackException e)
        {
            Assert.Catch<FailedAttackException>(() => throw e);
        }
    }
    [Test]
    public void ClericAttackThiefTest()
    {
        try
        {
            var espada = new MeleeWeapon("Espada de plata", 15, 22, 0, 0, 1, 2.1f);
            spore.clasf = new Cleric();
            spore.lvl = 30;
            spore.skills.armedCombat = 100;
            spore.hitPoints.item1 = 1;
            spore.hitPoints.item2 = 2;
            spore.weapon = espada;

            other.clasf = new Thief();
            other.lvl = 30;
            other.skills.combatTactics = 100;
            other.skills.shieldDefese = 100;
            other.shield = null;

            spore.Attack(other);

            var lifePointsExpected = new Range(163, 186).calculateRange();

            Assert.IsTrue(lifePointsExpected.Contains(other.state.lifePoints));
        }
        catch (FailedAttackException e)
        {
            Assert.Catch<FailedAttackException>(() => throw e);
        }
    }
    [Test]
    public void AttackWithAssasinDaggerTest()
    {
        try
        {
            var dagger = new Dagger("Daga +1", 3, 4, 1, 0.1f);
            spore.clasf = new Assassin();
            spore.lvl = 30;
            spore.skills.armedCombat = 100;
            spore.skills.stabbing = 100;
            spore.TakeItem(dagger);
            spore.UseItem("Daga +1");
            spore.hitPoints.item2 = 1;
            spore.hitPoints.item1 = 1;

            other.lvl = 30;
            other.skills.shieldDefese = 100;
            other.skills.combatTactics = 100;
            other.shield = null;
            spore.Attack(other);

            var lifeRangeStabExpected = new Range(229, 248).calculateRange();
            var lifeRangeWithoutStabExpected = new Range(274, 286).calculateRange();

            Assert.IsTrue(lifeRangeStabExpected.Contains(other.state.lifePoints) || lifeRangeWithoutStabExpected.Contains(other.state.lifePoints));
        }
        catch (FailedAttackException e)
        {
            Assert.Catch<FailedAttackException>(() => throw e);
        }
    }

    [Test]
    public void AttackWithWarriorDaggerTest()
    {
        try
        {
            var dagger = new Dagger("Daga +1", 3, 4, 1, 0.1f);
            spore.lvl = 30;
            spore.skills.stabbing = 100;
            spore.skills.armedCombat = 100;
            spore.TakeItem(dagger);
            spore.UseItem("Daga +1");
            spore.hitPoints.item1 = 1;
            spore.hitPoints.item2 = 1;

            other.lvl = 30;
            other.skills.combatTactics = 100;
            other.skills.shieldDefese = 100;
            other.shield = null;

            spore.Attack(other);

            var lifeRangeStabExpected = new Range(215, 228).calculateRange();
            var lifeRangeWithoutStabExpected = new Range(269, 278).calculateRange();

            Assert.IsTrue(lifeRangeStabExpected.Contains(other.state.lifePoints) || lifeRangeWithoutStabExpected.Contains(other.state.lifePoints));
        }
        catch (FailedAttackException e)
        {
            Assert.Catch<FailedAttackException>(() => throw e);
        }
    }

    [Test]
    public void AttackWithBanditWeaponTest()
    {
        try
        {
            var weapon = new MeleeWeapon("Espada larga", 4, 8, 0, 0, 1, 1.3f);
            spore.clasf = new Bandit();
            spore.lvl = 30;
            spore.skills.armedCombat = 100;
            spore.TakeItem(weapon);
            spore.UseItem("Espada larga");
            spore.hitPoints.item1 = 1;
            spore.hitPoints.item2 = 1;

            other.lvl = 30;
            other.skills.shieldDefese = 100;
            other.skills.combatTactics = 100;
            other.shield = null;

            spore.Attack(other);

            var lifeRangeCritExpected = new Range(217, 239).calculateRange();
            var lifeRangeWithoutCritExpected = new Range(255, 270).calculateRange();

            Assert.IsTrue(lifeRangeCritExpected.Contains(other.state.lifePoints) || lifeRangeWithoutCritExpected.Contains(other.state.lifePoints));
        }
        catch (FailedAttackException e)
        {
            Assert.Catch<FailedAttackException>(() => throw e);
        }
    }

    [Test]
    public void BeingAttacked()
    {
        spore.armor = new Armor("Placas completas", 15, 25, 0, 0, 1, 2.6f);
        spore.helmet = new Helmet("Almete de hierro", 5, 10, 0, 0, 1, 0.2f);
        spore.shield = new Shield("Placas completas", 1, 2, 0, 0, 1, 2.6f);

        var range = new Range(0, 15).calculateRange();

        spore.BeingAttacked(35);
        var lifeExpected = spore.state.lifePoints;

        Assert.IsTrue(range.Contains(lifeExpected));
    }

    [Test]
    public void DamageTest()
    {
        var damagesExpected = new Range(253, 296).calculateRange();

        spore.clasf = new Warrior();
        spore.hitPoints.item1 = 109;
        spore.hitPoints.item2 = 109;
        spore.weapon = new MeleeWeapon("Hacha de dos filos", 7, 20, 0, 0, 1, 2.1f);
        Assert.IsTrue(damagesExpected.Contains(spore.damage()));
    }

    [Test]
    public void TakeItemGold()
    {
        var goldExpected = 55;
        var gold = new Consumable("Gold", 0, 0, 0, 0, 0, goldExpected, 0f);
        spore.TakeItem(gold);
        Assert.AreEqual(goldExpected, spore.gold);
    }

    [Test]
    public void TakeItemExistingItem()
    {
        var inventoryQuantityExpected = 1;
        var redPotionQuantityExpected = 7;
        var item1 = new Consumable("Pocion Roja", 30, 0, 0, 0, 0, 3, 0f);
        var item2 = new Consumable("Pocion Roja", 30, 0, 0, 0, 0, 4, 0f);
        spore.TakeItem(item1);
        spore.TakeItem(item2);
        Assert.AreEqual(inventoryQuantityExpected, spore.inv.itemsAmount());
        Assert.AreEqual(redPotionQuantityExpected, spore.inv.fetchItem("Pocion Roja").quantity);

    }

    [Test]
    public void TakeItemNewItem()
    {
        var inventoryQuantityExpected = 2;
        var item1 = new Consumable("Pocion Azul", 0, 0.4f, 0, 0, 0, 4, 0f);
        var item2 = new Equipable("Espada Larga", 1, 0f, 0, 0);
        spore.TakeItem(item1);
        spore.TakeItem(item2);
        Assert.AreEqual(inventoryQuantityExpected, spore.inv.itemsAmount());
    }

    [Test]
    public void DropGoldTest()
    {
        var nameExpected = "Gold";
        var expectedAmount = 5;
        var gold = new Consumable("Gold", 0, 0, 0, 0, 0, 66, 0f);
        spore.TakeItem(gold);
        var goldDropped = spore.dropGold(5);
        Assert.AreEqual(nameExpected, goldDropped.name);
        Assert.AreEqual(expectedAmount, goldDropped.quantity);
    }

    [Test]
    public void LearnSpellTest()
    {
        var spellsAmountExpected = 2;
        var spell1 = new DirectDamage("Dardo magico", 1, 5, 10);
        var spell2 = new DirectDamage("Bomba magica", 6, 9, 40);
        var spell3 = new DirectDamage("Dardo magico", 1, 5, 10);
        spore.LearnSpell(spell1);
        spore.LearnSpell(spell2);
        spore.LearnSpell(spell3);

        Assert.AreEqual(spellsAmountExpected, spore.spells.Count());
    }

    [Test]
    public void CastSpellTest_DirectDamage()
    {
        var spell1 = new DirectDamage("Dardo magico", 1, 5, 10);
        var spell2 = new DirectDamage("Apocalipsis", 85, 100, 1000);

        spore.LearnSpell(spell1);
        spore.LearnSpell(spell2);

        spore.clasf = new Druid(); //El druida tiene un modificador de daño magico de 0.7f
        spore.lvl = 40;
        spore.attributes.intelligence = 22;
        spore.state.manaPoints = 2500;

        spore.castSpell(spell2, other);
        spore.castSpell(spell1, other);

        var lifesExpected = new Range(138, 168).calculateRange();
        var manaExpected = 1490;

        Assert.IsTrue(lifesExpected.Contains(other.state.lifePoints));
        Assert.AreEqual(manaExpected, spore.state.manaPoints);
    }

    [Test]
    public void CastSpellTest_DirectDamageWithMagicResist()
    {
        var spell1 = new DirectDamage("Dardo magico", 1, 5, 10);
        var spell2 = new DirectDamage("Apocalipsis", 85, 100, 1000);
        var magicalRing1 = new Magical("Gloom ring", 10, 0, 1, 0f);
        var magicalRing2 = new Magical("Espectral ring", 5, 0, 1, 0f);
        var magicalEarring = new Magical("Archer pendant", 10, 0, 1, 0f);

        other.TakeItem(magicalRing1);
        other.TakeItem(magicalRing2);
        other.TakeItem(magicalEarring);
        other.EquipItem(magicalRing1);
        other.EquipItem(magicalRing2);
        other.EquipItem(magicalEarring);

        spore.clasf = new Druid();
        spore.lvl = 40;
        spore.attributes.intelligence = 22;
        spore.state.manaPoints = 2500;
        spore.LearnSpell(spell1);
        spore.LearnSpell(spell2);

        spore.castSpell(spell2, other);

        var lifesExpected = new Range(171, 194).calculateRange();

        Assert.IsTrue(lifesExpected.Contains(other.state.lifePoints));
    }

    [Test]
    public void CastSpellTest_DirectDamageWithExtraDamage()
    {
        var crosier = new MeleeWeapon("Baculo de lazull", 8, 17, 0, 15, 1, 0.8f);
        var spell2 = new DirectDamage("Apocalipsis", 85, 100, 1000);

        spore.TakeItem(crosier);
        spore.UseItem("Baculo de lazull");
        spore.clasf = new Wizard();
        spore.lvl = 40;
        spore.attributes.intelligence = 22;
        spore.state.manaPoints = 2500;
        spore.LearnSpell(spell2);

        spore.castSpell(spell2, other);

        var lifesExpected = new Range(55, 98).calculateRange();

        Debug.Log("Daño magico extra de spore: " + spore.extraMagicDamage());

        Debug.Log(other.state.lifePoints);

        Assert.IsTrue(lifesExpected.Contains(other.state.lifePoints));
    }

    [Test]
    public void CastSpell_Healing()
    {
        var spell1 = new Healing("heal minor injuries", 6, 12, 10);
        var spell2 = new Healing("heal serious wounds", 30, 35, 25);

        spore.LearnSpell(spell1);
        spore.LearnSpell(spell2);

        spore.clasf = new Wizard();
        spore.lvl = 40;
        spore.attributes.intelligence = 22;
        spore.state.manaPoints = 2500;
        spore.state.lifePoints = 277;
        spore.state.maxLifePoints = 300;

        spore.castSpell(spell1, spore);
        spore.castSpell(spell2, spore);

        var lifeExpected = 300;

        Assert.AreEqual(lifeExpected, spore.state.lifePoints);
    }

    [Test]
    public void GainExperienceLevelUpTest()
    {
        spore.attributes.constitution = 21;
        spore.attributes.intelligence = 18;
        var lvlExpected = 2;
        var experienceExpected = 0;
        spore.GainExperience(152);
        Assert.AreEqual(experienceExpected, spore.xp);
        Assert.AreEqual(lvlExpected, spore.lvl);
    }

    [Test]
    public void GainExperienceTest()
    {
        var lvlExpected = 1;
        var experienceExpected = 60;
        spore.xpMax = 250;
        spore.GainExperience(60);
        Assert.AreEqual(experienceExpected, spore.xp);
        Assert.AreEqual(lvlExpected, spore.lvl);
    }

    [Test]
    public void NeedLvlUpTrueTest()
    {
        spore.xpMax = 55;
        spore.xp = 70;
        Assert.IsTrue(spore.needLevelUp(5));
    }

    [Test]
    public void NeedLvlUpFalseTest()
    {
        spore.xpMax = 55;
        spore.xp = 33;
        Assert.IsFalse(spore.needLevelUp(5));
    }

    [Test]
    public void LevelUpWarriorTest()
    {
        spore.attributes.constitution = 21;
        spore.attributes.intelligence = 18;
        var hitPointsExpected = new Tuple<int, int>(109, 109);
        var energyPointsExpected = 625;
        var manaPointsExpected = 0;
        var lifePointsExpected = new Range(334, 490).calculateRange();
        var actualExperienceExpected = 0;
        var maxExperienceExpected = 0;
        var lvlExpected = 40;

        for (int i = 1; i < 40; i += 1)
        {
            spore.LevelUp();
        }

        Assert.AreEqual(lvlExpected, spore.lvl);
        Assert.AreEqual(actualExperienceExpected, spore.xp);
        Assert.AreEqual(maxExperienceExpected, spore.xpMax);
        Assert.IsTrue(lifePointsExpected.Contains(spore.state.maxLifePoints));
        Assert.AreEqual(manaPointsExpected, spore.state.maxManaPoints);
        Assert.AreEqual(energyPointsExpected, spore.state.maxEnergyPoints);
        Assert.AreEqual(hitPointsExpected.item1, spore.hitPoints.item1);
        Assert.AreEqual(hitPointsExpected.item2, spore.hitPoints.item2);
    }

    [Test]
    public void BuyItemTest()
    {
        var goldExpected = 600;
        var itemsAmmount = 1;
        spore.gold = 55600;
        var itemToBuy = new Equipable("Espada de plata", 1, 0.4f, 0, 0);
        spore.BuyItem(55000, itemToBuy);
        Assert.AreEqual(goldExpected, spore.gold);
        Assert.AreEqual(itemsAmmount, spore.inv.itemsAmount());
    }

    [Test]
    public void SellItemTest()
    {
        var swordAmountExpected = 3;
        var goldExpected = 678000;
        var item = new Equipable("Espada MataDragones", 4, 3f, 0, 0);
        var item2 = new Equipable("Espada MataDragones", 1, 3f, 0, 0);
        spore.TakeItem(item);
        spore.SellItem(678000, item2);
        Assert.AreEqual(swordAmountExpected, spore.inv.inv.Find(i => i.name == item.name).quantity);
        Assert.AreEqual(goldExpected, spore.gold);
    }

    [Test]
    public void ChangeFactionTest()
    {
        var faction = new Faction();
        spore.ChangeFaction(faction);
        Assert.AreEqual(faction, spore.faction);
    }

    [Test]
    public void UseItemTest()
    {
        var weightExpected = 7.9f;
        var lifePointsExpected = 45;
        var redPotionAmountExpected = 3;
        var helmetDefenseExpected = new Tuple<int, int>(5, 10);
        var armorDefenseExpected = new Tuple<int, int>(45, 50);
        var shieldDefenseExpected = new Tuple<int, int>(1, 5);
        var weaponDamageExpected = new Tuple<int, int>(23, 25);
        var magicalDef = 60;

        var magicalRing = new Magical("The unique ring", 60, 60, 1, 0f);
        var potion = new Consumable("Red Potion", 30, 0, 0, 0, 0, 4, 0f);
        var weap = new MeleeWeapon("Dragon killer", 23, 25, 0, 0, 1, 3.4f);
        var helm = new Helmet("Champ Helmet", 5, 10, 0, 0, 1, 0.3f);
        var armo = new Armor("Black dragon armor", 45, 50, 0, 0, 1, 4.1f);
        var shie = new Shield("Tortuge shield", 1, 5, 0, 0, 1, 0.1f);

        spore.TakeItem(potion);
        spore.TakeItem(weap);
        spore.TakeItem(helm);
        spore.TakeItem(armo);
        spore.TakeItem(shie);
        spore.TakeItem(magicalRing);

        spore.UseItem("Red Potion");
        spore.UseItem("Champ Helmet");
        spore.UseItem("The unique ring");
        spore.UseItem("Dragon killer");
        spore.UseItem("Tortuge shield");
        spore.UseItem("Black dragon armor");


        Assert.AreEqual(lifePointsExpected, spore.state.lifePoints);
        Assert.AreEqual(redPotionAmountExpected, spore.inv.inv.Find(i => i.name == "Red Potion").quantity);
        Assert.AreEqual(weightExpected, spore.weight, 1f);
        Assert.AreEqual(weaponDamageExpected, spore.weapon.weapon);
        Assert.AreEqual(helmetDefenseExpected, spore.helmet.helmet);
        Assert.AreEqual(armorDefenseExpected, spore.armor.armor);
        Assert.AreEqual(shieldDefenseExpected, spore.shield.shield);
        Assert.AreEqual(magicalDef, spore.magicalItemsEquiped.sum(i => i.magicalDefense));
    }

    [Test]
    public void DropItemTest()
    {
        var itemDropped1NameExpected = "Blue Potion";
        var itemDropped1AmountExpected = 7;
        var itemDropped2NameExpected = "Armor";
        var itemDropped2AmountExpected = 1;
        var itemDropped3NameExpected = "Chicken";
        var itemDropped3AmountExpected = 55;
        var sporeItemsAmount = 1;

        var item1 = new Consumable("Blue Potion", 0, 0.4f, 0, 0, 0, 10, 0f);
        var item2 = new Consumable("Chicken", 0, 0, 5, 60, 0, 55, 0f);
        var item3 = new Equipable("Armor", 1, 2.3f, 0, 0);

        spore.TakeItem(item1);
        spore.TakeItem(item2);
        spore.TakeItem(item3);

        var itemDropped1 = spore.dropItem("Blue Potion", 7);
        var itemDropped2 = spore.dropItem("Armor", 1);
        var itemDropped3 = spore.dropItem("Chicken", 80);

        Assert.AreEqual(itemDropped1NameExpected, itemDropped1.name);
        Assert.AreEqual(itemDropped1AmountExpected, itemDropped1.quantity);
        Assert.AreEqual(itemDropped2NameExpected, itemDropped2.name);
        Assert.AreEqual(itemDropped2AmountExpected, itemDropped2.quantity);
        Assert.AreEqual(itemDropped3NameExpected, itemDropped3.name);
        Assert.AreEqual(itemDropped3AmountExpected, itemDropped3.quantity);
        Assert.AreEqual(sporeItemsAmount, spore.inv.itemsAmount());
    }

    [Test]
    public void StealTest()
    {
        var goldValuesExpected = new List<int>() { 0, 15 };
        spore.skills.steal = 100;

        other.gold = 500;

        spore.Steal(other);

        Assert.IsTrue(goldValuesExpected.Contains(spore.gold));
    }

    [Test]
    public void StealWithThiefTest()
    {
        spore.clasf = new Thief();
        spore.skills.steal = 100;
        var itemsNamesExpected = new List<string>() { "Red potion", "Dragon armor", "Bottle of water" };
        var goldValuesExpected = new List<int>() { 0, 15 };

        Consumable redPotion = new Consumable("Red potion", 30, 0, 0, 0, 0, 30, 0.5f);
        Armor dragonArmor = new Armor("Dragon armor", 45, 50, 0, 0, 1, 5.6f);
        Consumable bottleOfWater = new Consumable("Bottle of water", 00, 0, 0, 0, 10, 30, 0.5f);
        other.TakeItem(redPotion);
        other.TakeItem(dragonArmor);
        other.TakeItem(bottleOfWater);
        other.gold = 500;

        spore.Steal(other);

        Debug.Log("Cantidad de oro de Spore: " + spore.gold);
        Debug.Log("Cantidad de oro de la victima: " + other.gold);
        Debug.Log("Items de la victima: " + utils.listToString(other.inv.inv.ConvertAll(i => "(" + i.name + ", " + i.quantity + ")")));
        Debug.Log("Items de Spore: " + utils.listToString(spore.inv.inv.ConvertAll(i => "(" + i.name + ", " + i.quantity + ")")));

        Assert.IsTrue(goldValuesExpected.Contains(spore.gold) || itemsNamesExpected.Contains(spore.inv.inv[0].name));
    }

    [Test]
    public void ExamineLifePointsOfTestCase1()
    {
        spore.skills.survival = 7;

        Assert.AreEqual("Dudoso", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase2()
    {
        spore.skills.survival = 13;

        Assert.AreEqual("Sano", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase3()
    {
        spore.skills.survival = 13;

        other.state.lifePoints = 143;

        Assert.AreEqual("Herido", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase4()
    {
        spore.skills.survival = 25;

        other.state.lifePoints = 230;

        Assert.AreEqual("Sano", spore.examineLifePointsOf(other));
    }


    [Test]
    public void ExamineLifePointsOfTestCase5()
    {
        spore.skills.survival = 25;

        other.state.lifePoints = 169;

        Assert.AreEqual("Herido", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase6()
    {
        spore.skills.survival = 25;

        other.state.lifePoints = 149;

        Assert.AreEqual("Gravemente Herido", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase7()
    {
        spore.skills.survival = 37;

        other.state.lifePoints = 227;

        Assert.AreEqual("Sano", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase8()
    {
        spore.skills.survival = 37;

        other.state.lifePoints = 200;

        Assert.AreEqual("Levemente Herido", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase9()
    {
        spore.skills.survival = 37;

        other.state.lifePoints = 140;

        Assert.AreEqual("Herido", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase10()
    {
        spore.skills.survival = 37;

        other.state.lifePoints = 75;

        Assert.AreEqual("Gravemente Herido", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase11()
    {
        spore.skills.survival = 51;

        Assert.AreEqual("Intacto", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase12()
    {
        spore.skills.survival = 51;

        other.state.lifePoints = 299;

        Assert.AreEqual("Sano", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase13()
    {
        spore.skills.survival = 51;

        other.state.lifePoints = 200;

        Assert.AreEqual("Levemente Herido", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase14()
    {
        spore.skills.survival = 51;

        other.state.lifePoints = 140;

        Assert.AreEqual("Herido", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase15()
    {
        spore.skills.survival = 51;

        other.state.lifePoints = 45;

        Assert.AreEqual("Gravemente Herido", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase16()
    {
        spore.skills.survival = 51;

        other.state.lifePoints = 10;

        Assert.AreEqual("Casi Muerto", spore.examineLifePointsOf(other));
    }

    [Test]
    public void ExamineLifePointsOfTestCase17()
    {
        spore.skills.survival = 80;

        other.state.lifePoints = 191;

        Assert.AreEqual("(191/300)", spore.examineLifePointsOf(other));
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
        spore.TakeItem(rod);
        spore.UseItem(rod.name);

        spore.ExtractResource(shoal);

        var totalResourcesExpected = new Range(492, 499).calculateRange();

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
        spore.TakeItem(axe);
        spore.UseItem(axe.name);

        spore.ExtractResource(tree);

        var totalResourcesExpected = new Range(492, 499).calculateRange();

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
        spore.TakeItem(scissors);
        spore.UseItem(scissors.name);

        spore.ExtractResource(bush);

        var totalResourcesExpected = new Range(492, 499).calculateRange();

        Assert.IsTrue(spore.inv.existsItem("Raiz"));
        Assert.IsTrue(totalResourcesExpected.Contains(bush.resourceAmount));
    }


}