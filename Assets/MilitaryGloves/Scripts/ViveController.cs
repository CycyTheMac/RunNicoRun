using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    public GameObject glove;
    Animator gloveAni;
    

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
       
         gloveAni = glove.GetComponent<Animator>();    
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            
            gloveAni.SetBool("fist", false);
        }
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            
            gloveAni.SetBool("fist", true);
        }
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            gloveAni.SetBool("point", true);
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            gloveAni.SetBool("point", false);
        }
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
        {
            gloveAni.SetBool("stretch", true);
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip))
        {
            gloveAni.SetBool("stretch", false);
        }
        // Checks for Bottom Left Touchpad click
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && device.GetAxis().x < 0.0f && device.GetAxis().y < 0.0f)
        {
            gloveAni.SetBool("trigger", true);
        }
        

        // Checks for Bottom Right Touchpad click
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && device.GetAxis().x > 0.0f && device.GetAxis().y < 0.0f)
        {
            gloveAni.SetBool("holdmagazine", true);
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            gloveAni.SetBool("trigger", false);
            gloveAni.SetBool("holdmagazine", false);
        }

        //Debug.Log("Axis at " + device.GetAxis());
    }
}
