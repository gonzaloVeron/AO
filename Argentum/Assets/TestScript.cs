using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var attr = new Attributes(5, 0, 0, 0, 0, 0, 0);
        var ski = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        Character spore = new Character("Spore", attr, ski);
        Character other = new Character("Other", attr, ski);

        Faction fact1 = new Faction();
        Faction fact2 = new Faction();

        spore.ChangeFaction(fact1);
        spore.ChangeFaction(fact2);

        Debug.Log(spore.faction == fact2);

    }
}
