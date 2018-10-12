using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDraw_ColoredCross : DebugDraw_Cross {

    public Color _color = Color.cyan;
    void Update()
    {
        DebugDraw.duration = _duration == -1 ? Time.deltaTime : _duration;
        DebugDraw.Cross(_whereToDraw.position, _whereToDraw.rotation, _color);
    }
}
