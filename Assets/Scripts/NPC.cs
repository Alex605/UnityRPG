using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);

public delegate void CharacterRemoved();

public class NPC : Character
{

    public event HealthChanged healthChanged;

    public event CharacterRemoved characterRemoved;

    [SerializeField]    //each character will get their designated portrait from this
    private Sprite portrait;

    public Sprite MyPortrait
    {
        get
        {
            return portrait;
        }
    }
    public virtual void DeSelect()
    {

    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged !=  null) //only execute if we have a target
        {
            healthChanged(health);
        }
      
    }

    public  void OnCharacterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }

        Destroy(gameObject);
    }
}
