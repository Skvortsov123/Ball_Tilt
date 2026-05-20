using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Interactions;
using System;

public class PopUpFeedback : MonoBehaviour
{
    public TMP_Text text;

    private float timerShow;
    private float timerEffect;

    private const float SHOW_TEXT = 2; //show text in sec
    private const float DISAPPEAR_EFFECT = 0.5f;   //After show text, how long it takes to disapere in sec

    void Awake()
    {
        resetPop();
    }


    void Update()
    {
        if(Time.time - timerShow > SHOW_TEXT && timerEffect == 0)
        {
            timerEffect = Time.time;
        }

        if(timerEffect != 0)
        {
            float textAlpha = Mathf.Clamp01(1 - ((Time.time - timerEffect)/DISAPPEAR_EFFECT));    //Counts from 1 to 0 in "DISAPPEAR_EFFECT" seconds
            Color c = text.color;
            c.a = textAlpha;
            text.color = c;
        }

        if(text.color.a == 0)   //The text absolutli disapeared
        {
            resetPop();
        }
        //Reset timerEffect efter effekten är klar!
    }

    public void pop(String str) //print
    {
        resetPop();
        timerShow = Time.time;
        text.text = str;
    }

    private void resetPop()
    {
        timerShow = 0;
        timerEffect = 0;
        text.text = "";
        Color c = text.color;
        c.a = 1f;
        text.color = c;
    }
}
