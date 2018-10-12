using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplayer : MonoBehaviour {
    [SerializeField]
    private int lifeMax = 100;
    [SerializeField]
    private int life ;
    private Image lifeDisplay;
    private AudioSource audio;
    private float timer = 1f;
    private float temp;
    private bool played = false;

    public int Life {
        get {
            return life;
        }

        set {
            life = value;
        }
    }

    private void Start() {
        audio = GetComponent<AudioSource>();
        lifeDisplay = GetComponent<Image>();
        Life = lifeMax;
    }
    // Update is called once per frame
    void Update () {
        if (Life>lifeMax) {
            Life = lifeMax;
        }
        if (Life>lifeMax/3) {
            if (played) {
                audio.Pause();
                played = false;
            }
            lifeDisplay.color = Color.Lerp(new Color(1, 0, 0), new Color(0, 1, 0), (float)Life / lifeMax);           
        }        
        else{
            if (!played) {
                audio.Play();
                played = true;
            }
            
            temp += Time.deltaTime;
            if (temp > timer) {
                lifeDisplay.color = Color.Lerp(new Color(1, 0, 0), new Color(0, 1, 0), (float)Life / lifeMax);
                temp = 0;
            }
            if (temp>timer/2f) {
                lifeDisplay.color = Color.black;
            }
            
        }
	}
}
