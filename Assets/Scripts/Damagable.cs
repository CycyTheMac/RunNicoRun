using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{


    [SerializeField]
    private int m_pvMax;

    private int m_pv;


    public int Pv
    {
        get
        {
            return m_pv;
        }

        protected set
        {
            m_pv = value;
        }
    }


    // Use this for initialization
    void Awake()
    {
        Pv = m_pvMax;

    }

    virtual public void Damage(int p_dommages)
    {

        Pv = Pv - p_dommages;
        if (Pv <= 0)
        {
            Death();
        }
    }

    virtual protected void Death()
    {

        Debug.Log(gameObject.name + " es mort ! ");
        Destroy(gameObject);
    }


}