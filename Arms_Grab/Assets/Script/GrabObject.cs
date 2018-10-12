using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabObject : MonoBehaviour {

    public bool m_isTrigger;
    private GameObject m_objet;
    [SerializeField]
    private Transform m_Take;

    

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
        if (pick())
        {
            m_isTrigger = true;
        }

    }

    protected abstract bool pick();

    //private void OnTriggerExit(Collider other)
    //{
    //    m_isTrigger = false;
    //}


    private void Drag()
    {

        if (IsDragging())
        {
            m_Take.position = transform.position;
            m_Take.rotation = transform.rotation;
            m_objet.GetComponent<Collider>().isTrigger = true;
            m_objet.GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (!IsDragging())
        {
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

    protected override bool pick()
    {
        throw new System.NotImplementedException();
    }
}
public class GrabOVR : GrabObject
{
    protected override bool IsDragging()
    {
        return OVRInput.Get(OVRInput.RawButton.RHandTrigger);
    }

    protected override bool pick()
    {
        return  OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
    }
}
