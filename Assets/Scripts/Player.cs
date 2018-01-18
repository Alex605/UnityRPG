using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [SerializeField]
    private Stat mana;



    private float maxMana = 50;

    [SerializeField]
    private Transform[] exitPoints;     //Exit points for attacking

    private int exitIndex = 2;      //index for exitPoints. Starts at 2 because 2 = exitPoint Down

    private SpellBook spellBook;


    [SerializeField]
    private Block[] blocks;

    public Transform myTarget { get; set; }    //Targets an character


    protected override void Start()
    {
        spellBook = GetComponent<SpellBook>();
        
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

        if (Input.GetKey(KeyCode.W))    //Move up
        {
            exitIndex = 0;
            direction += Vector2.up;
            StopAttack();   
        }
        if (Input.GetKey(KeyCode.A))    //Move left
        {
            exitIndex = 3;
            direction += Vector2.left;
            StopAttack();
        }
        if (Input.GetKey(KeyCode.S))    //Move down
        {
            exitIndex = 2;
            direction += Vector2.down;
            StopAttack();
        }
        if (Input.GetKey(KeyCode.D))    //Move right
        {
            exitIndex = 1;
            direction += Vector2.right;
            StopAttack();
        }
    }

    //helps with attacking
    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = myTarget; //makes it so the player can't change their target mid-attack & stops attack if target dies

        //Creates a new spell so that we can use the information from it to cast the spell in game
        Spell newSpell = spellBook.CastSpell(spellIndex);

        isAttacking = true; //indicates if we are attacking

        myAnimator.SetBool("attack", isAttacking);


        yield return new WaitForSeconds(newSpell.MyCastTime); //hardcoded cast time

        if (currentTarget != null && InLineOfSight())
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();  //Uses the spell associated with the spellIndex

            s.Initialize(currentTarget, newSpell.MyDamage);
        }

        StopAttack(); //end the current attack
    }

    // function for casting a spell
    public void CastSpell(int spellIndex)
    {
        Block();

        if (myTarget != null && !isAttacking && !IsMoving && InLineOfSight())  //Checks if player is able to attack
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));   //Coroutine lets you do something while the rest of the script runs
        }

    }

    private bool InLineOfSight()   //Only allows the player to cast spells on target's that he is facing
    {
        if (myTarget != null)
        {
            Vector3 targetDirection = (myTarget.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, myTarget.transform.position), 256);  //Only works on Unity layer 256 aka the Block layer

            if (hit.collider == null)
            {
                return true;
            }
        }
   
        return false;
    }

    private void Block()    //Blocks attacking view of player
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();   //Exit index keeps track of the direction we're facing and changes line of sight.
    }

    public override void StopAttack()
    {
        spellBook.StopCasting(); //stops the spell if the player moves

        base.StopAttack(); // call the character stop attack aswell
    }
}
