using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {
    public OVRInput.Controller Controller;

    public Animator HandAnimator;

    public OVRInput.Touch IndexTouch;
    public OVRInput.Axis1D IndexAxis;
    public float IndexAxisFloat;

    public OVRInput.Axis1D HandAxis;
    public float HandAxisFloat;

    public OVRInput.Touch ThumbrestTouch;
    public bool Thumbrest = false;

    public float targetValue = 0f;
    public float fistSpeed = 8f;

    void Update () {
        transform.localPosition = OVRInput.GetLocalControllerPosition(Controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(Controller);
        IndexAxisFloat = OVRInput.Get(IndexAxis, OVRInput.Controller.Touch);
        HandAxisFloat = OVRInput.Get(HandAxis, OVRInput.Controller.Touch);

        if (OVRInput.GetDown(IndexTouch))
        {
            HandAnimator.SetBool("TouchTrigger", true);
        }
        if(OVRInput.GetUp(IndexTouch))
        {
            HandAnimator.SetBool("TouchTrigger", false);
            
        }

        if (OVRInput.GetDown(ThumbrestTouch))
        {
            Thumbrest = true;
            HandAnimator.SetBool("Thumbrest", Thumbrest);
        }
        if (OVRInput.GetUp(ThumbrestTouch))
        {
            Thumbrest = false;
            HandAnimator.SetBool("Thumbrest", Thumbrest);
        }

        if (IndexAxisFloat > 0.5  && Thumbrest && HandAxisFloat > 0.5 ) {
            if (targetValue < 1)
            {
                targetValue += fistSpeed * Time.deltaTime;
            }
        } else
        {
            if(targetValue > 0) {
            targetValue -= fistSpeed * Time.deltaTime;
            }
            
        }
        HandAnimator.SetLayerWeight(3, Mathf.Lerp(0f, 1f, targetValue));
        CheckHold();
    }

    void CheckHold()
    {
        HandAnimator.SetFloat("IndexBlend", IndexAxisFloat);
        HandAnimator.SetLayerWeight(2, HandAxisFloat);
    }
}
