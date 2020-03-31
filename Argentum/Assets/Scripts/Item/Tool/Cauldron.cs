using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Tool
{
    public Cauldron(int quantity, float weight) : base("Cauldron", quantity, weight)
    {
        this.quantity = quantity;
        this.weight = weight;
    }
    public override Item whatSubstract(FountainOfResources res, int value, int amount) => res.extractWithAxe(0, 0); //Esto es para que rompa, no tiene que hacer nada

    public override void Use(Player other)
    {
        //Aca deberia abrir la ventana de creacion de items de alquimia
        GameObject.Find("AlchemyWindow").GetComponent<AlchemyWindow>().Activate();
    }

}
