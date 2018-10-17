using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSphere : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log(other);
    }
}
