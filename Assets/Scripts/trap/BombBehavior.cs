using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

    public enum bombType {paralyzed,slow,respawn}
    public bombType bombe;



    IsTrapable obj;
    [SerializeField] private float m_radius;

    /*
     *  On trigger enter,cast a sphere around the player, verify if the object IsTrappable and
     *  if it is, call it's notifyAsTrapped method
     */

    public IEnumerator Start() {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            CheckAround();
        }

    }

    private void CheckAround()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_radius);

        foreach (var item in hitColliders)
        {
            obj = item.GetComponent<IsTrapable>();
            if (obj)
            {
                //Debug.Log("trapable object hit");
                obj.NotifyAsTrapped(bombe); // notify the lutin that he has been trapped by a bomb of type "bombe"
                Kill(); // destoy the bomb
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //generate a sphere around the play and fill an array with object in it;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_radius);

        foreach (var item in hitColliders)
        {
            obj = item.GetComponent<IsTrapable>();
            if (obj)
            {
                //Debug.Log("trapable object hit");
                obj.NotifyAsTrapped(bombe); // notify the lutin that he has been trapped by a bomb of type "bombe"
                Kill(); // destoy the bomb
            }
        }
    }

    private void Kill()
    {
        Destroy(this.gameObject);
    }

    // Default value
    private void Reset()
    {
        m_radius = 3.7f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.5f, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, m_radius);
    }


}
