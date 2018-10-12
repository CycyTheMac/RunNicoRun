using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// this script should be on any object to can trigger a trap

// put the lutin object in the object Input in the inspector 
// change the function to trapBehavior -> hasBeenTrapped

public class IsTrapable : MonoBehaviour {

    private UnityEvent onTrapped;

    public OnTrappedEvent onTrappedWithInfo;


    [Serializable]
    public class OnTrappedEvent: UnityEvent<BombBehavior.bombType>{}
    
    // When called, will execute event given in IsTrapable component
    public void NotifyAsTrapped()
    {
        onTrapped.Invoke();
    }

    internal void NotifyAsTrapped(BombBehavior.bombType bombe)
    {
        //Debug.Log("Notifyied as trap with type: " + bombe);
        onTrappedWithInfo.Invoke(bombe);
    }
}
