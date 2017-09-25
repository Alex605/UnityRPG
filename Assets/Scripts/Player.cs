using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;

    private float maxHealth = 100;

    private float maxMana = 50;

    protected override void Start()
    {
        health.Initalize(maxHealth, maxHealth); //Initalize health values;
        mana.Initalize(maxMana, maxMana);
        base.Start();
    }
    // Update is called once per frame
    protected override void Update () {
        GetInput();
        
        base.Update();  // Executes Character.Update to use Move()
	}

    private void GetInput()
    {
        direction = Vector2.zero;   //Setting direction to zero prevents the program from moving faster and faster. 

       if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
       if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }

        if (Input.GetKey(KeyCode.W))    //Move up
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))    //Move left
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))    //Move down
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))    //Move right
        {
            direction += Vector2.right;
        }

    }
}
