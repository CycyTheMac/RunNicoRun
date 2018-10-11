using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItemController : MonoBehaviour {

    Vector3 offset;
    bool grab = false;
    Queue<Vector3> lastPosition;
    [SerializeField]
    GameObject leftAnkor;

    private void Start() {
        //init the queue of last positions
        lastPosition = new Queue<Vector3>();
    }

    private void OnTriggerStay(Collider other) {
        //check if we grab the ball
        if (/*(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)
            ||*/ (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) 
            && other.CompareTag("pickupAble")) {
            //if not already grabed, we said it's grab
            if (!grab) {
                grab = true;
                //offset = other.transform.position - gameObject.transform.position;
                //remove gravity and add the kinematic option
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
            }
            //set the position of the ball into the hand
            other.transform.position = gameObject.transform.position + 
                (other.GetComponent<SphereController>().Taille.x * 
                (-gameObject.transform.right + (-gameObject.transform.up)/5));

            //remove the position too much
            if (lastPosition.Count >= 10) {
                lastPosition.Dequeue();
            }
            //add the recent position
            lastPosition.Enqueue(other.transform.position);
        } else {
            if (grab) {
                //re-add the gravity and remove the kinematic option
                other.GetComponent<Rigidbody>().useGravity = true;
                other.GetComponent<Rigidbody>().isKinematic = false;
                //calculate the vector for the power of the shot
                Vector3 lastVector = other.transform.position - lastPosition.Peek();
                //add the force to the ball
                other.GetComponent<Rigidbody>().AddForce(
                    (leftAnkor.transform.forward - leftAnkor.transform.up) * Vector3.Distance(other.transform.position, lastPosition.Peek()) * 300);
            }
            //set grab to false when we leave the ball
            grab = false;
        }
    }
}
