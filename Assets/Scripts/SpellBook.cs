using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour {

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text spellName;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Spell[] spells;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private Coroutine spellRoutine;

    private Coroutine fadeRoutine;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Spell CastSpell(int index)
    {

        castingBar.fillAmount = 0; //always start cast time at 0
        castingBar.color = spells[index].MyBarColor; //Changes color of casting bar depending on the spell being cast.
        spellName.text = spells[index].MyName; //gets the name of the spell being cast and puts it in the casting bar. 
        icon.sprite = spells[index].MyIcon; //gets the icon of the spell being cast and puts it in the casting bar. 

        spellRoutine = StartCoroutine(Progress(index));

        fadeRoutine = StartCoroutine(FadeBar());

        return spells[index];   //returns spell from the array
    }

    private IEnumerator Progress(int index) //Index tells us how much time we need to cast the spell
    {
        float timePassed = Time.deltaTime; //when we start
        
        float rate = 1.0f / spells[index].MyCastTime;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (spells[index].MyCastTime - timePassed).ToString("F2");

            if (spells[index].MyCastTime - timePassed < 0)
            {
                castTime.text = "";
            }
            yield return null; //helps make this run as fast as possible
        }

        StopCasting(); //stop casting if player moves
    }
    private IEnumerator FadeBar()
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / 0.50f; //fade over .5 seconds

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null; //helps make this run as fast as possible
        }
    }

    public void StopCasting()
    {
        if (fadeRoutine != null) //gets the bar off the screen once the spell has finished casting
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
            fadeRoutine = null;
        }
        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }
}
