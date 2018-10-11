using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {


    Vector3 offset;
    bool grab = false;
    


   

    private void OnTriggerStay(Collider other)
    {
        float Axe_indexTrigger = Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger");
        Debug.Log("test is on the trigger-- "+Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger"));
        //check if we grab the ball
        if (Axe_indexTrigger > 0.1f)
            ///*&& other.CompareTag("pickupAble")*/)
      {
            Debug.Log("test dans if grab");
            //if not already grabed, we said it's grab
            if (!grab)
            {
                Debug.Log("this is", other);

                grab = true;
                GiveGravityToObject(other, false);
            }
            else
            {
                //set the position of the ball into the hand
                DefineObjectPosition(other);
            }
        }
        else
        {
            if (grab)
            {

                GiveGravityToObject(other, true);
            }
            //set grab to false when we leave the ball
            grab = false;
        }
    }

    private void DefineObjectPosition(Collider other)
    {
        other.transform.position = gameObject.transform.position +
            (gameObject.GetComponent<Transform>().position.x *
            (-gameObject.transform.right + (-gameObject.transform.up) / 5));
    }

    private static void GiveGravityToObject(Collider other, bool gravityOn)
    {
        //remove gravity and add the kinematic option
        Rigidbody r = other.attachedRigidbody;
        r.useGravity = gravityOn;
        r.isKinematic = !gravityOn;
    }
}
