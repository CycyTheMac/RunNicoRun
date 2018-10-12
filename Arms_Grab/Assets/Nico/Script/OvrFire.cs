using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvrFire : Fire
    {
        public override bool input()
        {
            return OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger);
        }
    }

