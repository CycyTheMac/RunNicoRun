using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour {

    [SerializeField]
    PoolManager m_poolManager;

    private void Start() {
        //Instantiate(player, transform.position, Quaternion.identity);
    }

    public void PlayerSpawn() {
        if (!m_poolManager) {
            m_poolManager = FindObjectOfType<PoolManager>();
        }

        GameObject l_player = m_poolManager.GetFromPool("Player");
        l_player.transform.position = transform.position;
        l_player.GetComponent<PlayerController>().Initialize();
    }
}
