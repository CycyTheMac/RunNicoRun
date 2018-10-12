using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTransform : MonoBehaviour {

    public Transform m_affected;
    public Transform m_toMirror;

	void Update () {
        m_affected.position = m_toMirror.position;
        m_affected.rotation = m_toMirror.rotation;

    }
}
