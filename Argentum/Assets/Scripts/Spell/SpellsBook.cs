using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsBook
{
    public HashSet<Spell> book;

    public SpellsBook()
    {
        this.book = new HashSet<Spell>();
    }

    public void Add(Spell s)
    {
        if (!this.contains(spell => spell.name == s.name))
        {
            this.book.Add(s);
        }
    }

    public bool contains(Func<Spell, bool> condition)
    {
        bool cont = false;
        foreach(Spell s in this.book)
        {
            cont = cont || condition(s); 
        }
        return cont;
    }

    public int Count() => this.book.Count;
}
