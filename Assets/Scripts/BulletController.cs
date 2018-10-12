using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private int m_strength = 100;
    [SerializeField]
    private float m_tempsVie = 5;
    private Rigidbody m_bullet;
    private float m_time;
    // Use this for initialization

    private void Update()
    {
        Destroy(gameObject, 5f);
    }


    private void OnCollisionEnter(Collision p_collision)
    {
        Damagable l_damage = p_collision.gameObject.GetComponent<Damagable>();
        if (l_damage)
        {
            l_damage.Damage(1);
        }
        Destroy(gameObject, 5f);
    }

    internal void initialise()
    {
        if (!m_bullet)
        {
            m_bullet = GetComponent<Rigidbody>();
        }
        m_bullet.velocity = Vector3.zero;
        m_bullet.AddForce(transform.forward * m_strength, ForceMode.Impulse);
    }
}
