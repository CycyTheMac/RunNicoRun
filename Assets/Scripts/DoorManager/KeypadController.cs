using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadController : MonoBehaviour {

    private string curPassword;
    public string input = "";
    public bool doorOpen;
    public Transform doorHinge;

    public Text displayPassword;
    public int passwordLenght = 3;
    public float highlightTime = 0.5f;

    public PadButtonController[] controllers;

    public void ResetNumber()
    {
        input = "";
    }
  
    private void Start()
    {
        curPassword = "";

        for (int i = 0; i < passwordLenght; i++)
        {
            curPassword += UnityEngine.Random.Range(0, 10);
        }

        Debug.Log(curPassword);

        StartCoroutine(BlinkSequence());
    }

    private IEnumerator BlinkSequence()
    {

        while (true)
        {
            char[] passwordChar = curPassword.ToCharArray();

            for(int i = 0; i < passwordChar.Length; i++)
            {
                int index=0;
                switch (passwordChar[i])
                {
                    case '0': index = 0; break;
                    case '1': index = 1; break;
                    case '2': index = 2; break;
                    case '3': index = 3; break;
                    case '4': index = 4; break;
                    case '5': index = 5; break;
                    case '6': index = 6; break;
                    case '7': index = 7; break;
                    case '8': index = 8; break;
                    case '9': index = 9; break;

                    default:
                        break;
                }
                yield return new WaitForSeconds(1);
                controllers[index].Highlight(0.5f);
            }
        }
    }

    public void AddNumber(int number)
    {
        input += number;
    }

    private void Update()
    {
        displayPassword.text = input;

        if(input.Length >= passwordLenght)
        {
            CheckIfPasswordGood();
            ResetNumber();
        }

        if(doorOpen)
        {
            var newRotation = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 250);
            doorHinge.rotation = newRotation;
        }
    }

    private void CheckIfPasswordGood()
    {
        if (input == curPassword)
        {
            doorOpen = true;
        }
    }
}