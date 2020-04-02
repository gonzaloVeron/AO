using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Player spore;
    // Start is called before the first frame update

    public Vector2 mov;

    public Rigidbody2D rb2d;

    public float speed;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = 15;

        var attr = new Attributes(0, 0, 0, 0, 0, 0, 0);
        var skills = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        spore = new Player("Spore", attr, skills, new Wizard());

        var sword = new MeleeWeapon("Espada larga", 4, 8, 0, 0, 1, 0.4f);
        spore.lvl = 30;
        spore.skills.armedCombat = 11;
        spore.hitPoints.item1 = 1;
        spore.hitPoints.item2 = 1;
        spore.TakeItem(sword);
        spore.UseItem("Espada larga");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Creature ch = new Creature("Alguien", 500, new Tuple<int, int>(0, 0), 0, 0, 0, 5, null);
        this.hacerQueAtaque(ch);
        this.moverFlaquito();
    }

    public void hacerQueAtaque(Character ch)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("TOCASTE LA A MAESTRO");
            spore.Attack(ch);
        }
    }
    public void moverFlaquito()
    {
        mov = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
    }
}
