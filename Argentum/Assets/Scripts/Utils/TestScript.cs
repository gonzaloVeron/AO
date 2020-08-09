using System.Collections;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using UnityEngine;
using MongoDB.Bson.Serialization;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public Player cha;

    public PlayerService playerService;

    public PotionService potionService;

    public RecipeService recipeService;

    public ResourceService resourceService;

    public ItemEquipableService ieService;

    // Start is called before the first frame update
    void Start()
    {
        //playerService = new PlayerService();
        //potionService = new PotionService();
        //recipeService = new RecipeService();
        //resourceService = new ResourceService();
        //ieService = new ItemEquipableService();

        //var attr = new Attributes(0, 0, 0, 0, 0, 0, 0);
        //var skills = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);


        //Recipe r1 = new Recipe("Lingote de plata", 50, "Forge");
        //r1.AddElement("Mineral de plata", 20);

        //recipeService.Save(r1);

    }

    /* Script para crear un color a partir de un string que representa un numero hexadecimal
    private int HexToDec(string hex) => System.Convert.ToInt32(hex, 16);

    private float HexToFloatNormalized(string hex) => HexToDec(hex) / 255f;

    private Color GetColorFromString(string hexString) => new Color(HexToFloatNormalized(hexString.Substring(0, 2)), HexToFloatNormalized(hexString.Substring(2, 2)), HexToFloatNormalized(hexString.Substring(4, 2)));
    */

}
