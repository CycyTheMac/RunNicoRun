using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabObject : MonoBehaviour {

    public bool m_isTrigger;
    private GameObject m_objet;
    [SerializeField]
    private Transform m_Take;

    private void Awake() {
        if (!m_Take) {
            m_Take = GameObject.FindObjectOfType<OvrFire>().transform;
        }
    }

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

    private void Drag()
    {

        if (IsDragging())
        {
            m_objet.transform.parent =m_Take;
            m_Take.position = transform.position + new Vector3(0, 0.1f, 0);
            m_Take.rotation = transform.rotation;
            m_objet.GetComponent<Collider>().isTrigger = true;
            m_objet.GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (!IsDragging())
        {
            m_objet.transform.parent = null;
            m_objet.GetComponent<Collider>().isTrigger = false;
            m_objet.GetComponent<Rigidbody>().isKinematic = false;
            m_isTrigger = false;

        }
    }
    protected abstract bool IsDragging();
}



