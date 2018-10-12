using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fire : MonoBehaviour {
    [SerializeField]
    private GameObject m_bullet;
    [SerializeField]
    private Transform m_origine;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (input())
        {
            fire();

        }


    }

    public abstract bool input();

    private void fire()
    {
        m_bullet = Instantiate(m_bullet, m_origine.position, m_origine.rotation);
    }


}



