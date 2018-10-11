using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabObject : MonoBehaviour {

    public bool m_isTrigger;
    private GameObject m_objet;
    

    

    // Update is called once per frame
    void Update () {
        //RaycastHit l_hit;

        if (m_objet)
        {
            if (m_isTrigger)
            {
                m_objet.GetComponent<Rigidbody>().useGravity = false;
                Drag();
            }
            else
            {
                m_objet.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("pickupAble"))
        {
            m_objet = other.gameObject;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            m_isTrigger = true;
        }
        
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    m_isTrigger = false;
    //}


    private void Drag()
    {

        if (IsDragging())
        {
            m_objet.transform.parent = transform;
            m_objet.GetComponent<Collider>().isTrigger = true;
            m_objet.GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {
            m_objet.transform.parent = null;
            m_objet.GetComponent<Collider>().isTrigger = false;
            m_objet.GetComponent<Rigidbody>().isKinematic = false;
            m_isTrigger = false;

        }
    }

    protected abstract bool IsDragging();
}

public class GrabObjectDefaultExample : GrabObject
{
    public KeyCode m_keyToGrab;
    protected override bool IsDragging()
    {
        return Input.GetKey(m_keyToGrab);
    }
}
