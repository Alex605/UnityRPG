using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;

    [SerializeField]
    private Text statValue;

    private float currentFill;

    [SerializeField]
    private float lerpSpeed;

    public float MyMaxValue {get; set; }
        
    private float currentValue;

    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {

            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }

            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue; 

            if (statValue != null)
            {
                statValue.text = currentValue + " / " + MyMaxValue; //makes sure we update the value text
            }
          
        }

    }

    void Start()
    {
        content = GetComponent<Image>();
    }

    void Update()
    {
        content.fillAmount = currentFill;
    }

    public void Initalize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }


}

