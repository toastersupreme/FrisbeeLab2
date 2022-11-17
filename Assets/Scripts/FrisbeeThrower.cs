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
    public int throwCount = 0;
    public string scoreTitle;

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
        throwCount++;
    }

    public void Score()
    {
        if (throwCount == 1)
        {
            scoreTitle = "Albatross";
        }
        else if (throwCount == 2)
        {
            scoreTitle = "Eagle";
        }
        else if (throwCount == 3)
        {
            scoreTitle = "Birdie";
        }
        else if (throwCount == 4)
        {
            scoreTitle = "Par";
        }
        else if (throwCount == 5)
        {
            scoreTitle = "Bogey!";
        }
        else if (throwCount == 6)
        {
            scoreTitle = "Double Bogey!";
        }
        else if (throwCount <= 7)
        {
            scoreTitle = "Triple Bogey!";
        }

        //something should go here to display the players score when they get the frisbee in the goal thing

        if (scoreTitle == "Triple Bogey!")
        {
            //end the game i guess idk im too tired
        }
    }
}
