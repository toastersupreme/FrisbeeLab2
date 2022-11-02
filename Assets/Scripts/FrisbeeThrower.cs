using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeThrower : MonoBehaviour
{
    public float ThrowStrength;
   
    public GameObject RightHand;
    public Transform MainBody, Frisbee;
    public Animator animator;
    private Rigidbody Rigidbody;
    private Quaternion holdingState;

    private void Start()
    {
        Rigidbody = Frisbee.GetComponent<Rigidbody>();
        holdingState = Frisbee.transform.rotation;
    }
    public void PickUp()
    {
        Frisbee.transform.position = RightHand.transform.position;
        Frisbee.transform.rotation = holdingState;
        Frisbee.transform.parent = RightHand.transform;
        Rigidbody.isKinematic = true;
    }
    public void ThrowFrisbee()
    {
        //throw it forword lol
        Frisbee.transform.parent = null;
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(MainBody.right * ThrowStrength, ForceMode.Impulse);
    }
}
