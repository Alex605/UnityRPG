using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))] //adds a rigid body by default
[RequireComponent(typeof(Animator))] //adds an animator by default

//abstract class that all characters need to inherit from
public abstract class Character : MonoBehaviour {

    // The Player's movement speed
    [SerializeField]        //Allows me to access speed in Unity without making speed public.
    private float speed;

    protected Animator myAnimator;

    //Player's direction
    protected Vector2 direction;

    private Rigidbody2D myRigidbody;

    protected bool isAttacking = false;

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;

    [SerializeField]
    protected Stat health;

    public Stat MyHealth
    {
        get { return health; }
    }
    [SerializeField]
    private float maxHealth;

    public bool IsMoving    //Allows me to check if character is moving around.
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Use this for initialization
    protected virtual void Start () {

        health.Initalize(maxHealth, maxHealth); //Initalize health values;

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        HandleLayers();
	}

    private void FixedUpdate()
    {
        Move();
    }
 

    //Moves the character
    public void Move()
    {
        //Normalized limits the vector so that the character doesn't move quicker diagonally when pressing two keys.
        myRigidbody.velocity = direction.normalized * speed;    

    }

    //Handles animation layers.
    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("WalkLayer");

            //Sets the animation parameter so that the character faces the correct direction.
            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            isAttacking = false;
        }
        else if(true)   //attack Layer
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            //If not moving, set back to idle.
            ActivateLayer("IdleLayer");
        }
    }

    public void AnimateMovement(Vector2 direction)
    {
        
    }

    public void ActivateLayer(string layerName) //Activates specified animation layer
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public virtual void StopAttack()    //ends the current attack
    {
        isAttacking = false; //Make sure that we are not attacking

        myAnimator.SetBool("attack", isAttacking); //Stops the attack animation

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        //health reduce
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0) //if health reaches 0, character dies
        {
            myAnimator.SetTrigger("Die"); // if condition met, play death animation
        }
    }
}
