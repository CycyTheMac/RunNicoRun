using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDraw_Path : MonoBehaviour {

    public Transform _trackedObject;
    public float _recordInterval=0.1f;
    public Color _pathColor = Color.green;

    [Header("Debug")]
    public bool _useDebug=true;
    public List<Vector3> _positions;

    private void Awake()
    {
        if (_trackedObject == null)
            DestroyImmediate(this);
    }

    IEnumerator Start () {
        while (true)
        {
            yield return new WaitForSeconds(_recordInterval);
            RecordPosition();
        }
		
	}

    private void RecordPosition()
    {
        _positions.Add(_trackedObject.position);
        if (_useDebug)
        {
            DebugDraw.duration = _recordInterval;
            DebugDraw.Path(_positions);

        }
    }
    

    void Reset () {
        _trackedObject = this.transform;
    }
}
