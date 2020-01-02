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

        Item item1 = new Consumible("Pocion Roja", 30, 0, 0, 0, 0, 0, 1);
        Item itemg1 = new Consumible("Gold", 0, 0, 0, 0, 0, 530, 0);
        Item item2 = new Consumible("Pocion Roja", 30, 0, 0, 0, 0, 0, 1);
        Item item3 = new Consumible("Pocion Roja", 30, 0, 0, 0, 0, 0, 1);
        Item itemg2 = new Consumible("Gold", 0, 0, 0, 0, 0, 3346, 0);
        Item itemA = new Consumible("Pocion Azul", 0, 0.4f, 0, 0, 0, 0, 1);
        Item item4 = new Consumible("Pocion Roja", 30, 0, 0, 0, 0, 0, 1);
        Item itemg3 = new Consumible("Gold", 0, 0, 0, 0, 0, 981, 0);
        Item item5 = new Consumible("Pocion Roja", 30, 0, 0, 0, 0, 0, 1);
        Item espada1 = new Equipable("Espada Larga", 0, 0, 0, 0, 0, 0, 4, 8, 1);
        Item armadura = new Equipable("Armadura de placas completas", 15, 30, 0, 0, 0, 0, 0, 0, 1);
        Item espada2 = new Equipable("Espada Larga", 0, 0, 0, 0, 0, 0, 4, 8, 2);

        spore.TakeItem(item1);
        spore.TakeItem(itemg1);
        spore.TakeItem(item2);
        spore.TakeItem(item3);
        spore.TakeItem(itemg2);
        spore.TakeItem(itemA);
        spore.TakeItem(item4);
        spore.TakeItem(itemg3);
        spore.TakeItem(item5);
        spore.TakeItem(espada1);
        spore.TakeItem(armadura);
        spore.TakeItem(espada2);

        Debug.Log("Cantidad de pociones rojas: " + spore.inventory.Find(i => i.name == "Pocion Roja").quantity);
        
        spore.UseItem("Pocion Roja");
        spore.UseItem("Pocion Roja");

        Debug.Log("Vida de Spore: " + spore.state.lifePoints);
        Debug.Log("Cantidad de objetos: " + spore.inventory.Count);
        Debug.Log("Cantidad de pociones rojas: " + spore.inventory.Find(i => i.name == "Pocion Roja").quantity);
        Debug.Log("Cantidad de pociones azules: " + spore.inventory.Find(i => i.name == "Pocion Azul").quantity);
        Debug.Log("Cantidad de oro de Spore: " + spore.gold);
        Debug.Log("Cantidad de espadas largas: " + spore.inventory.Find(i => i.name == "Espada Larga").quantity);
    }
}
