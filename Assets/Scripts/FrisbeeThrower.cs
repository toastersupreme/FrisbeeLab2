using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class FrisbeeThrower : MonoBehaviour
{
    public float ThrowStrength;
   
    public GameObject RightHand;
    public Transform MainBody, Frisbee;
    public Animator animator;
    private Rigidbody Rigidbody;
    private Quaternion holdingState;

    [Range(0f, 1f)]
    public float throwPercentage;
    private void Start()
    {
        Rigidbody = Frisbee.GetComponent<Rigidbody>();
        holdingState = Frisbee.transform.rotation;
        UiManager.Instance.m_OutOfBounds.AddListener(PickUp);
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
        throwPercentage = UiManager.Instance.powerValue();
        Frisbee.transform.parent = null;
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce((ThrowStrength * throwPercentage) * MainBody.right, ForceMode.Impulse);
    }
}
