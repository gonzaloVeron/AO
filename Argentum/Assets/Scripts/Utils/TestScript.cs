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

    public PotionService potionService;

    public RecipeService recipeService;

    public ResourceService resourceService;

    // Start is called before the first frame update
    void Start()
    {
        //playerService = new PlayerService();
        //potionService = new PotionService();
        //recipeService = new RecipeService();
        //resourceService = new ResourceService();

        //var attr = new Attributes(0, 0, 0, 0, 0, 0, 0);
        //var skills = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);


    }
}
