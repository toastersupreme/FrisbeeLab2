using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeThrower : MonoBehaviour
{
    public float ThrowStrength;
    public GameObject Frisbee;
    public GameObject RightHand;
    public Transform MainBody, offset;
    public Animator animator;
    private Rigidbody Rigidbody;
    

    private void Start()
    {

        Rigidbody = Frisbee.GetComponent<Rigidbody>();
        
}
    public void PickUp()
    {
        
        
        Frisbee.transform.transform.position = RightHand.transform.position; //+ offset.position;
        Frisbee.transform.parent = RightHand.transform;
        Rigidbody.useGravity = false;
            
        
    }
    public void ThrowFrisbee()
    {
        
        
        //var AnimatorClip = animator.GetCurrentAnimatorClipInfo(0);
        //float animationLength = AnimatorClip[0].clip.length;
        //waitLength(animationLength);



        Frisbee.transform.parent = null;

        Rigidbody.AddForce(MainBody.forward * ThrowStrength, ForceMode.Impulse);

        Rigidbody.useGravity = true;
        //throw it forword lol
    }
    IEnumerator waitLength(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
