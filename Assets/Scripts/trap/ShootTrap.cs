using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Place this script on the leftHandAnchor of the OVRCamera rig

public class ShootTrap : MonoBehaviour {
    
    [SerializeField] GameObject[] bomb_prefabs;
    [SerializeField] private float m_fireRate;
    private float m_time = 0f;

    private void Update()
    {
        m_time += Time.deltaTime;

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            //Debug.Log("index Trigger hit");

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && m_time > m_fireRate)
            {
                //Debug.Log("collider hit");
                Instantiate(bomb_prefabs[Random.Range(0,bomb_prefabs.Length)],hit.point + Vector3.up * 0.5f,Quaternion.identity);
                m_time = 0f;
            }
        }
    }

    private void Reset()
    {
        m_fireRate = 3f;
    }

}
