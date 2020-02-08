﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class CharacterTestCase
{
    private Character spore;

    private Character other;

    private Attributes attributesSpore;

    private Attributes attributesOther;

    private Skills skillsSpore;

    private Skills skillsOther;

    [SetUp]
    public void SetUp()
    {
        attributesSpore = new Attributes(40, 38, 0, 0, 0, 0, 0);
        attributesOther = new Attributes(40, 38, 0, 0, 0, 0, 0);
        skillsSpore = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        skillsOther = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        spore = new Character("Spore", attributesSpore, skillsSpore, new Warrior());
        other = new Character("Other", attributesOther, skillsOther, new Warrior());
        other.armor = new Armor("Vestimenta Simple", 3, 6, 1, 0f);
        other.helmet = new Helmet("Capucha", 2, 5, 1, 0f);
        other.shield = new Shield("Escudo de madera roto", 1, 2, 1, 0f);
        other.weapon = new MeleeWeapon("Daga rota", 1, 1, 1, 0f);
        other.state.lifePoints = 300;

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
            var espada = new MeleeWeapon("Espada larga rota", 1, 1, 1, 0.5f);
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
            var espada = new MeleeWeapon("Espada de plata", 15, 22, 1, 2.1f);
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

            var lifeRangeStabExpected = new Range(234, 248).calculateRange();
            var lifeRangeWithoutStabExpected = new Range(276, 286).calculateRange();

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
            var weapon = new MeleeWeapon("Espada larga", 4, 8, 1, 1.3f);
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
        spore.armor = new Armor("Placas completas", 15, 25, 1, 2.6f);
        spore.helmet = new Helmet("Almete de hierro", 5, 10, 1, 0.2f);
        spore.shield = new Shield("Placas completas", 1, 2, 1, 2.6f);

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
        spore.weapon = new MeleeWeapon("Hacha de dos filos", 7, 20, 1, 2.1f);
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
        var item2 = new Equipable("Espada Larga", 1, 0f);
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

        spore.lvl = 40;
        spore.attributes.intelligence = 22;
        spore.state.manaPoints = 2500;

        other.state.lifePoints = 300;

        spore.castSpell(spell2, other);
        spore.castSpell(spell1, other);

        var lifesExpected = new Range(138, 168).calculateRange();
        var manaExpected = 1490;

        Assert.IsTrue(lifesExpected.Contains(other.state.lifePoints));
        Assert.AreEqual(manaExpected, spore.state.manaPoints);
    }

    [Test]
    public void CastSpell_Healing()
    {
        var spell1 = new Healing("heal minor injuries", 6, 12, 10);
        var spell2 = new Healing("heal serious wounds", 30, 35, 25);

        spore.LearnSpell(spell1);
        spore.LearnSpell(spell2);

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
        
        for(int i = 1; i < 40; i += 1)
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
        var itemToBuy = new Equipable("Espada de plata", 1, 0.4f);
        spore.BuyItem(55000, itemToBuy);
        Assert.AreEqual(goldExpected, spore.gold);
        Assert.AreEqual(itemsAmmount, spore.inv.itemsAmount());
    }

    [Test]
    public void SellItemTest()
    {
        var swordAmountExpected = 3;
        var goldExpected = 678000;
        var item = new Equipable("Espada MataDragones", 4, 3f);
        var item2 = new Equipable("Espada MataDragones", 1, 3f);
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
        var weap = new MeleeWeapon("Dragon killer", 23, 25, 1, 3.4f);
        var helm = new Helmet("Champ Helmet", 5, 10, 1, 0.3f);
        var armo = new Armor("Black dragon armor", 45, 50, 1, 4.1f);
        var shie = new Shield("Tortuge shield", 1, 5, 1, 0.1f);

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
        var item3 = new Equipable("Armor", 1, 2.3f);

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
    
    
}
