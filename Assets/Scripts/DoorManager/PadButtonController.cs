﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadButtonController : MonoBehaviour {

    public int number;

    public KeypadController keypad;
    public LayerMask m_layersAllowed;
    public float timeHighlight = 1f;
    public float timeSet = 1f;
    public bool push = false;
    public float timerPush = 0f;
    public float timeToWait = 10f;

    public Color m_blinkColor= Color.red;
    public Color m_defaultColor= Color.white;

    private void OnTriggerEnter(Collider other) {
        if (timerPush <= 0) {
            keypad.AddNumber(number);
            push = true;
            timerPush = timeToWait;

            while (push) {
                timerPush -= Time.deltaTime;
                if (timerPush <= 0) {
                    push = false;
                }
            }
            
        }
    }

    public void OnMouseDown()
    {
        keypad.AddNumber(number);
    }

    private void Update()
    {
        DecreaseTimeHighlight();
    }

    private void DecreaseTimeHighlight()
    {
        if (timeHighlight == 0)
            return;
        
        timeHighlight -= Time.deltaTime;

        float pourcentHighLight = timeHighlight / timeSet;
        
        GetComponent<Renderer>().material.color = Color.Lerp(m_blinkColor, m_defaultColor, pourcentHighLight);

        if (timeHighlight < 0)
            timeHighlight = 0;
    }

    public void Highlight(float highlightTime)
    {
        this.timeHighlight =timeSet  = highlightTime;
    }

    /*public static bool IsInLayerMask(int layer, LayerMask layermask)
 {
   return layermask == (layermask | (1 << layer));
 }*/
}
