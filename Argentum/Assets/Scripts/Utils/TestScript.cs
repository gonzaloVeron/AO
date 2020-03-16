using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Player cha;

    public PlayerService sv;

    // Start is called before the first frame update
    void Start()
    {
        sv = new PlayerService();

        var attr = new Attributes(0, 0, 0, 0, 0, 0, 0);
        var skills = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        Player spore = sv.fetchPlayer("Spore");

        spore.attributes.intelligence = 22;

        sv.UpgradePlayer(spore);
    
    }
}
