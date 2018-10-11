using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {


    Vector3 offset;
    bool grab = false;
    Queue<Vector3> lastPosition;
    [SerializeField]
    GameObject leftAnkor;

    private void Start()
    {
        //init the queue of last positions
        lastPosition = new Queue<Vector3>();
    }

    private void OnTriggerStay(Collider other)
    {
        //check if we grab the ball
        if (
            (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            && other.CompareTag("pickupAble"))
        {
            //if not already grabed, we said it's grab
            if (!grab)
            {
                grab = true;
                //remove gravity and add the kinematic option
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
            }
            //set the position of the ball into the hand
            other.transform.position = gameObject.transform.position +
                (other.GetComponent<Transform>().position.x *
                (-gameObject.transform.right + (-gameObject.transform.up) / 5));

            //remove the position too much
            if (lastPosition.Count >= 10)
            {
                lastPosition.Dequeue();
            }
            //add the recent position
            lastPosition.Enqueue(other.transform.position);
        }
        else
        {
            if (grab)
            {
                //re-add the gravity and remove the kinematic option
                other.GetComponent<Rigidbody>().useGravity = true;
                other.GetComponent<Rigidbody>().isKinematic = false;
            }
            //set grab to false when we leave the ball
            grab = false;
        }
    }
}
