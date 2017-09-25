using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    // The Player's movement speed
    [SerializeField]        //Allows me to access speed in Unity without making speed public.
    private float speed;

    private Animator myAnimator;

    //Player's direction
    protected Vector2 direction;

    private Rigidbody2D myRigidbody;

    public bool IsMoving    //Allows us to see if character is moving around.
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Use this for initialization
    protected virtual void Start () {
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

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }
}
