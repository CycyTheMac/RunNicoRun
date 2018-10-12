using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MoveOne : MonoBehaviour {

    // Déplacements normaux.

    [SerializeField]
    Transform centerEye;

    float vitesse;
    bool isRotate = false;

    private void Update()
    {
        //Vitesse de déplacement en fonction de l'inclinaison avant ou arrière du joyStick de la manette gauche.
        if (Input.GetAxis("Vertical") > 0)
        {
            vitesse = Input.GetAxis("Vertical");
            transform.GetComponent<Rigidbody>().AddForce(centerEye.forward * vitesse * 500 * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            vitesse = Input.GetAxis("Vertical");
            transform.GetComponent<Rigidbody>().AddForce(centerEye.forward * vitesse * 300 * Time.deltaTime);
        }

        //Rotation de 45° à gauche ou à droite, seulement si l'inclinaison du JoyStick de la manette gauche est assez importante (éviter de tourner sans le vouloir).
        if (isRotate == false && (Input.GetAxis("Horizontal") > 0.5f))
        {
            isRotate = true;
            transform.Rotate(new Vector3(0, 45, 0));
        }
        if (isRotate == false && (Input.GetAxis("Horizontal") < -0.5f))
        {
            isRotate = true;
            transform.Rotate(new Vector3(0, -45, 0));
        }
        else if (isRotate == true && Input.GetAxis("Horizontal") <= 0.5f && Input.GetAxis("Horizontal") >= -0.5f)
        {
            isRotate = false;
        }
    }
}
