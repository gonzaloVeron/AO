using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class LimitedListTestCase
{
    private LimitedList<Spell> list;

    private Healing spell1;

    private Healing spell2;

    private Healing spell3;

    private Healing spell4;


    [SetUp]
    public void SetUp()
    {
        list = new LimitedList<Spell>(3);
        spell1 = new Healing("Curar heridas leves", 6, 12, 10);
        spell2 = new Healing("Curar heridas graves", 30, 35, 25);
        spell3 = new Healing("Fuente de vida", 70, 90, 350);
        spell4 = new Healing("Pregonar la palabra de dios", 135, 160, 560);

    }

    [Test]
    public void AddTest()
    {
        var sizeExpected = 2;
        list.Add(spell1);
        list.Add(spell2);

        Assert.AreEqual(sizeExpected, list.size);
    }

    [Test]
    public void AddTestThrowIndexOutOfRangeException()
    {
        Assert.Catch<System.IndexOutOfRangeException>(() => { list.Add(spell1); list.Add(spell2); list.Add(spell3); list.Add(spell4); });
    }
    
    [Test]
    public void RemoveTest()
    {
        var indexNullExpected = 1;
        var sizeExpected = 2;
        list.Add(spell1);
        list.Add(spell2);
        list.Add(spell3);

        list.Remove(spell2);

        Assert.AreEqual(sizeExpected, list.size);
        Assert.IsNull(list.array[indexNullExpected]);
    }

    [Test]
    public void FindTest()
    {
        list.Add(spell1);
        list.Add(spell2);
        list.Add(spell4);

        var spellNameFinded = "Pregonar la palabra de dios";

        var maxDamageFinded = 35;

        var spellExpected = list.Find(s => s.maxDamage == maxDamageFinded);

        var spellExpected_ = list.Find(s => s.name == spellNameFinded);

        Assert.AreEqual(spellExpected, spell2);
        Assert.AreEqual(spellExpected_, spell4);
    }

    [Test]
    public void FindTestThrowArgumentNullException()
    {
        var spellNameFinded = "Pregonar la palabra de dios";
        
        list.Add(spell1);
        list.Add(spell2);
        list.Add(spell3);

        Assert.Catch<System.ArgumentNullException>(() => { list.Find(s => s.name == spellNameFinded); });
    }
}
