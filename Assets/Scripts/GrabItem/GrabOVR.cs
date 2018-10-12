using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GrabOVR : GrabObject
    {

        protected override bool IsDragging()
        {
            return OVRInput.Get(OVRInput.RawButton.RHandTrigger);
        }

        protected override bool pick()
        {
            return
                OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);
        }
}
