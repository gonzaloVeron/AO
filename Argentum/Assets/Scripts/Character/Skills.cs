using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
    public int combatTactics;
    public int armedCombat;
    public int martialArts;
    public int stabbing; //Apuñalar
    public int throwingWeapons;
    public int projectileWeapons;
    public int shieldDefese;
    public int magic;
    public int magicResist;
    public int meditating;
    public int hiding;
    public int tameAnimals;
    public int music;
    public int steal;
    public int trade;
    public int survival;
    public int leadership;
    public int fishing;
    public int mining; //Este lo hace fede
    public int cutDownTrees;
    public int botany;
    public int smithy; //Herreria
    public int carpentry;
    public int alchemy;
    public int tailoring; //Sastreria
    public int navigation;
    public int riding; //Equitacion

    public Skills(int combatTactics, int armedCombat, int martialArts, int stabbing, int throwingWeapons, int projectileWeapons,
        int shieldDefese, int magic, int magicResist, int meditating, int hiding, int tameAnimals, int music, int steal, int trade,
        int survival, int leadership, int fishing, int mining, int cutDownTrees, int botany, int smithy, int carpentry, int alchemy,
        int tailoring, int navigation, int riding)
    {
        this.combatTactics = combatTactics;
        this.armedCombat = armedCombat;
        this.martialArts = martialArts;
        this.stabbing = stabbing;
        this.throwingWeapons = throwingWeapons;
        this.projectileWeapons = projectileWeapons;
        this.shieldDefese = shieldDefese;
        this.magic = magic;
        this.magicResist = magicResist;
        this.meditating = meditating;
        this.hiding = hiding;
        this.tameAnimals = tameAnimals;
        this.music = music;
        this.steal = steal;
        this.trade = trade;
        this.survival = survival;
        this.leadership = leadership;
        this.fishing = fishing;
        this.mining = mining;
        this.cutDownTrees = cutDownTrees;
        this.botany = botany;
        this.smithy = smithy;
        this.carpentry = carpentry;
        this.alchemy = alchemy;
        this.tailoring = tailoring;
        this.navigation = navigation;
        this.riding = riding;
    }
    
}
