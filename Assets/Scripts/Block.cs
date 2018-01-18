using System;
using UnityEngine;

//Gives the character a line of sight
//Players may only attack enemies that they are looking at

[Serializable]
public class Block
{

    [SerializeField]
    private GameObject first, second;

    public void Deactivate()
    {
        first.SetActive(false);
        second.SetActive(false);
    }

    public void Activate()
    {
        first.SetActive(true);
        second.SetActive(true);
    }

}
