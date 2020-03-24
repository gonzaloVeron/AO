using System.Collections;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine;
using MongoDB.Bson.Serialization;

public class TestScript : MonoBehaviour
{
    public Player cha;

    public PlayerService playerService;

    // Start is called before the first frame update
    void Start()
    {
        playerService = new PlayerService();

        var attr = new Attributes(0, 0, 0, 0, 0, 0, 0);
        var skills = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        //playerService.CreatePlayer("Spore", attr, skills, new Wizard());

        //Player spore = playerService.fetchPlayer("Spore");

        //spore.attributes.intelligence = 22;

        //playerService.UpgradePlayer(spore);

        //Debug.Log(spore.name);

    }
}
