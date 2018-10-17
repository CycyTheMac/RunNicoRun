using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Integrate effect that the bomb will have in this file
// this script is placed on the lutin (navMeshAgent)
public class TrapBehaviors : MonoBehaviour {


    public float paralyzeTime;
    public float slowDownTime;
    private float p_time;
    private float s_time;

    private NavMeshAgent m_lutin;

    RespawnTag[] spawnPoints;

    private void Start()
    {
        m_lutin = GetComponent<NavMeshAgent>();
    }

    IEnumerator Paralyze(float l_time)
    {
        m_lutin.isStopped = true;
        yield return new WaitForSeconds(l_time);
        m_lutin.isStopped = false;
    }

    IEnumerator SlowDown(float l_time)
    {
        //Debug.Log(" slowing down");
        m_lutin.speed = m_lutin.speed / 2;
        yield return new WaitForSeconds(l_time);
        m_lutin.speed = m_lutin.speed * 2;
    }

    private void Respawn()
    {
        //Debug.Log("Respawning");
        spawnPoints = GameObject.FindObjectsOfType<RespawnTag>();
        //Debug.Log(spawnPoints.Length);
        RespawnTag tag = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        //we use Warp because it's a function of navMesh
        m_lutin.Warp(tag.transform.position);
        //debugBall.position = tag.transform.position;
        
    }


    public void HasBeenTrapped(BombBehavior.bombType bombe) {

        if (bombe == BombBehavior.bombType.paralyzed)
            StartCoroutine(Paralyze(paralyzeTime));
        if (bombe == BombBehavior.bombType.slow)
            StartCoroutine(SlowDown(slowDownTime));
        if (bombe == BombBehavior.bombType.respawn)
            Respawn();
    }

    private void Reset()
    {
        p_time = 0;
        s_time = 0;
    }
}
