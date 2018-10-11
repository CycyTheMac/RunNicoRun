using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour {

    [SerializeField]
    GameObject m_enemy;

    [SerializeField]
    Transform m_spawnMaxX;

    [SerializeField]
    Transform m_spawnMaxZ;

    [SerializeField]
    LevelManager m_lvlManager;

    [SerializeField]
    EnemyManager m_enemyType;


    float m_timeToWait = 0.15f;
    int m_maxEnemey = 3;
    float m_timer;
    float m_min = 0;
    float m_maxX;
    float m_maxY;
    float m_maxZ;
    protected PoolManager _poolManager;
    [SerializeField]
    LayerMask m_layerTouched;
    //max detection of the raycast (400 in case of hole in terrain)
    float m_maxRayDistance = 400;
    [SerializeField]
    float m_detectionRadius = 1f;


    public bool m_useDebug;

    // Use this for initialization
    void Start () {
        //define the range of the spawn
        m_maxX = m_spawnMaxX.position.z;
        m_maxY = m_spawnMaxX.position.y;
        m_maxZ = m_spawnMaxZ.position.x;
    }

    // Update is called once per frame
    void FixedUpdate () {
        m_timer -= Time.deltaTime;
        if (m_timer <= 0f) {
            Spawn();
        }
    }

    void Spawn() {
        if (!_poolManager) {
            _poolManager = FindObjectOfType<PoolManager>();
        }
        float l_positionX;
        float l_positionY;
        float l_positionZ;
        Vector3 l_spawnPos;
        bool l_raycastResult = false;
        RaycastHit l_hit;
        GameObject l_enemy;

        for (int i = 0; i < m_maxEnemey; i++) {
            l_raycastResult = false;
            //loop while the point's spawn is different from the terrain
            do {
                l_positionX = Random.Range(m_min, m_maxX);
                l_positionY = m_maxY;
                l_positionZ = Random.Range(m_min, m_maxZ);
                l_spawnPos = new Vector3(l_positionX, l_positionY, l_positionZ);
                //Instantiate<GameObject>(m_enemy, l_spawnPos, Quaternion.identity);

                //calculation of the ray for the position
                Ray l_ray = new Ray(l_spawnPos, Vector3.down);
                Color l_color = Color.red;
                //check if there is an hit with other thing than the terrain
                if (Physics.SphereCast(l_ray, m_detectionRadius, out l_hit, m_maxRayDistance)) {
                 
                    //define if the raycast don't hit the terrain layer
                    
                    if (!IsInLayerMask(l_hit.collider.gameObject.layer, m_layerTouched) ) {
                        l_color = Color.green;

                        l_raycastResult = true;
                    }
                }

                if(m_useDebug)
                    Debug.DrawLine(l_spawnPos, l_spawnPos + Vector3.down * m_maxRayDistance, l_color, 20);

            } while (!l_raycastResult);

            //create and place the enemy on the terrain
            l_enemy = _poolManager.GetFromPool(m_enemy.name);
            //Correct the hit point by 4 in the y axe
            l_enemy.transform.position = l_hit.point + new Vector3(0, 4, 0);

            //initialise randomly the enemy type that was created
            int l_i = Random.Range(0, 3);
            l_enemy.GetComponent<EnemyController>().Initialise(m_enemyType.Data[l_i]);
        }
        m_timer = m_timeToWait;
    }

    public static bool IsInLayerMask(int layer, LayerMask layermask) {
        return layermask == (layermask | (1 << layer));
    }
}
