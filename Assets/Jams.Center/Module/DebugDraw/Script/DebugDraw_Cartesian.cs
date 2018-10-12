using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDraw_Cartesian : MonoBehaviour {

    public Transform _whereToDraw;
    public Space _type = Space.Self;
    public float _lenght=1f;
    public float _duration = -1;

    private void Awake()
    {
        if (_whereToDraw == null)
            DestroyImmediate(this);
    }
    void Update () {
        DebugDraw.duration = _duration == -1 ? Time.deltaTime : _duration;
        DebugDraw.Cartesian(_whereToDraw.position, _whereToDraw.rotation, _lenght);
	}
    private void Reset()
    {
        _whereToDraw = transform;
    }
}
