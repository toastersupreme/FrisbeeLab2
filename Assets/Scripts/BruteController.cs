using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class BruteController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float turnSpeed = 0.5f;
    [SerializeField]
    private float runSpeed = 1f;

    private bool runBool = false;

    private bool holdingFrisbee;

    private FrisbeeThrower ft;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ft = GetComponent<FrisbeeThrower>();
        holdingFrisbee = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Run", true);
            runBool = true;
        }
        else
        {
            animator.SetBool("Run", false);
            runBool = false;
        }

        if (runBool == true)
        {
            runSpeed = 2;
        }
        else
        {
            runSpeed = 1;
        }

        //pick up
        if (Input.GetKeyDown(KeyCode.R) && holdingFrisbee == false)
        {
            animator.SetTrigger("Pick Up");
            ft.PickUp();
            holdingFrisbee = true;
        }

        //throw
        if (Input.GetKeyDown(KeyCode.T) && holdingFrisbee == true)
        {
            animator.SetTrigger("Throw");

            float animationLength = (animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);


            AddEvent(1, 0.2f, "Throw", 0);

            waitLength(animationLength);

            ft.ThrowFrisbee();
            holdingFrisbee = false;
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float speed = moveSpeed * Input.GetAxis("Vertical");

        controller.SimpleMove(forward * speed * runSpeed);

        animator.SetFloat("Speed", speed);

    }

    IEnumerator waitLength(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void AddEvent(int Clip, float time, string functionName, float floatParameter)
    {
        animator = GetComponent<Animator>();
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = functionName;
        animationEvent.floatParameter = floatParameter;
        animationEvent.time = time;
        AnimationClip clip = animator.runtimeAnimatorController.animationClips[Clip];
        clip.AddEvent(animationEvent);
    }
}

