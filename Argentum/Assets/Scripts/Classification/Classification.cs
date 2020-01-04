using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Classification
{
    public abstract int calculateLifePoints(int constitution);
    public abstract int calculateManaPerLevel(int intelligence);
    public abstract int initialMana();
    public abstract int hitPointsPerLevel(int characterLvl);
    public abstract float meleeDamageMod();
    public abstract float meleeAimMod();
    public abstract float projectileWeaponDamageMod();
    public abstract float projectileWeaponAimMod();
    public abstract float withoutWeaponDamageMod();
    public abstract float withoutWeaponAimMod();
    public abstract float defenseShieldMod();
    public abstract float defenseEvasionMod();
    public int energyPerLevel()
    {
        return 15;
    }

}
