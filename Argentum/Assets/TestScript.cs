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
        var spore = new Character("Spore", attr, ski);

        var item1 = new Consumable("Pocion Roja", 30, 0, 0, 0, 0, 0, 2);
        var item2 = new Consumable("Pocion Roja", 30, 0, 0, 0, 0, 0, 3);
        var item3 = new Consumable("Pocion Roja", 30, 0, 0, 0, 0, 0, 7);
        var item4 = new Consumable("Pocion Roja", 30, 0, 0, 0, 0, 0, 1);
        var item5 = new Equipable("Espada Larga", 0, 0, 0, 0, 0, 0, 4, 8, 1);

        spore.TakeItem(item1);
        spore.TakeItem(item2);
        spore.TakeItem(item3);
        spore.TakeItem(item4);
        spore.TakeItem(item5);

        Item i = spore.dropItem("Pocion Roja", 5);

        Debug.Log(spore.inv.fetchItem("Pocion Roja").quantity);
        Debug.Log(spore.inv.inv.Count);
        Debug.Log("Item tirado: " + i.name + "," + "cantidad: " + i.quantity);

    }
}
