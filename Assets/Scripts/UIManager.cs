using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour

{

    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }
    //reference to all the action buttons
    [SerializeField]
    private Button[] actionButtons;

    //Keycodes used for executing the action buttons
    private KeyCode action1, action2, action3;

    [SerializeField]
    private GameObject targetFrame;

    [SerializeField]
    private Image portraitFrame;

    private Stat healthStat;

	// Use this for initialization
	void Start ()
    {

        healthStat = targetFrame.GetComponentInChildren<Stat>();

        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }
        if (Input.GetKeyDown(action2))
        {
            ActionButtonOnClick(1);
        }
        if (Input.GetKeyDown(action3))
        {
            ActionButtonOnClick(2);
        }

    }

    private void ActionButtonOnClick(int btnIndex)
    {
        actionButtons[btnIndex].onClick.Invoke();
    }

    public void ShowTargetFrame(NPC target) //Shows the health bar of the target we have selected
    {
        targetFrame.SetActive(true);

        healthStat.Initalize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);

        portraitFrame.sprite = target.MyPortrait;

        target.healthChanged += new HealthChanged(UpdateTargetFrame);


    }

    public void HideTargetFrame() //hides our previous target's health bar after we click on something else
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTargetFrame(float value) //updates the current health number in the health bar of the current target
    {
        healthStat.MyCurrentValue = value;
    }
}
