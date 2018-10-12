using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTwo : MonoBehaviour {

    //Déplacements par mini-téléportation.

    [SerializeField]
    Transform centerEye;

    [SerializeField]
    int distance = 3;

    Ray myRayForward;
    RaycastHit hitInfoForward;

    [SerializeField]
    GameObject ForwardPoint;

    Ray myRayBack;
    RaycastHit hitInfoBack;

    [SerializeField]
    GameObject BackPoint;

    bool onMove = false;
    bool isRotate = false;
    bool hasHitForward;
    bool hasHitBack;

    // Update is called once per frame
    void Update () {

        //On regarde s'il n'y a pas d'obstacle devant nous.
        myRayForward = new Ray(transform.position, centerEye.forward);
        hasHitForward = Physics.Raycast(transform.position, centerEye.forward, out hitInfoForward, distance);

        //On regarde s'il n'y a pas d'obstacle derrière nous.
        myRayBack = new Ray(transform.position, -centerEye.forward);
        hasHitBack = Physics.Raycast(transform.position, -centerEye.forward, out hitInfoBack, distance / 2);

        //Deplacement là où on regarde par mini-téléportation, s'il n'y a pas d'obstacle. Plus la vitesse est grande, plus on est téléporté loin.
        if (Input.GetAxis("Vertical") > 0.5f && onMove == false && hasHitForward == false)
        {
            onMove = true;
            TeleportForward();
        }
        else if (Input.GetAxis("Vertical") < -0.5f && onMove == false && hasHitBack == false)
        {
            onMove = true;
            Vector3 yZero = centerEye.forward;
            yZero.y = 0;
            transform.Translate(yZero * -distance / 2, Space.World);
        }

        //Si il y a un obstacle.

        else if (Input.GetAxis("Vertical") > 0.5f && onMove == false && hasHitForward == true)
        {
            onMove = true;
                //On recupère la position de l'object avec lequel on entre en collision, que l'on soustrait de notre position actuelle, pour avoir un vecteur direction qui va à l'opposer du mur.
            Vector3 directionHitToPlayer = GetPositionPlayer() - hitInfoForward.point;
                //On normalise la direction pour ramener le vecteur à une longueur de 1.
            Vector3 directionHitToPlayerNormalized = directionHitToPlayer.normalized;
                //Debug.DrawLine(hitInfoForward.point, hitInfoForward.point + directionHitToPlayerNormalized, Color.red, 15);
                //On récupère la distance séparant le "milieu" du joueur avec un point placé à l'avant (son ventre par exemple).
            float epaisseurJoueur = Vector3.Distance(transform.position, ForwardPoint.transform.position);
                //On multiplie la direction par la distance obtenue au-dessus pour obtenir un vecteur d'une longueur adéquate, afin que le joueur ne traverse pas l'obstacle, et qu'il ne se téléporte pas dedans. 
            Vector3 directionAvecEpaisseur = directionHitToPlayerNormalized * epaisseurJoueur;
                //On téléporte le joueur juste devant le mur.
            transform.position = hitInfoForward.point + directionAvecEpaisseur;
        }
        else if (Input.GetAxis("Vertical") < -0.5f && onMove == false && hasHitBack == true)
        {
            onMove = true;
            Vector3 directionHitToPlayer = GetPositionPlayer() - hitInfoBack.point;
            Vector3 directionHitToPlayerNormalized = directionHitToPlayer.normalized;
            float epaisseurJoueur = Vector3.Distance(transform.position, BackPoint.transform.position);
            Vector3 directionAvecEpaisseur = directionHitToPlayerNormalized * epaisseurJoueur;
            transform.position = hitInfoBack.point + directionAvecEpaisseur;
        }

        //On remet à false le mouvement pour ne pas avoir un mouvement continu.

        else if (onMove == true && Input.GetAxis("Vertical") <= 0.5f && Input.GetAxis("Vertical") >= -0.5f)
        {
            onMove = false;
        }

        //Rotation de 45° à gauche ou à droite, seulement si l'inclinaison du JoyStick de la manette gauche est assez importante (éviter de tourner sans le vouloir).
        else if (isRotate == false && (Input.GetAxis("Horizontal") > 0.5f))
        {
            isRotate = true;
            transform.Rotate(new Vector3(0, 45, 0));
        }
        else if (isRotate == false && (Input.GetAxis("Horizontal") < -0.5f))
        {
            isRotate = true;
            transform.Rotate(new Vector3(0, -45, 0));
        }
        else if (isRotate == true && Input.GetAxis("Horizontal") <= 0.5f && Input.GetAxis("Horizontal") >= -0.5f)
        {
            isRotate = false;
        }
    }

    private Vector3 GetPositionPlayer()
    {
        return transform.position;
    }

    private void TeleportForward()
    {
        Vector3 directionTeleportation = TakeDirectionOfTeleport();
        transform.Translate(directionTeleportation * distance, Space.World);
    }

    private Vector3 TakeDirectionOfTeleport()
    {
        Vector3 directionTeleportation = centerEye.forward;
        directionTeleportation.y = 0;
        return directionTeleportation;
    }
}
