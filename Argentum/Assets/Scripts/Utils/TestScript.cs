using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Character cha;

    // Start is called before the first frame update
    void Start()
    {
        var attr = new Attributes(0, 0, 0, 0, 0, 0, 0);
        var skills = new Skills(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        cha = new Character("Spore", attr, skills, new Wizard());

    }
}
