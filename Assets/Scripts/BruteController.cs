using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController), typeof(Animator),typeof(FrisbeeThrower))]
public class BruteController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField]
    private GameObject Cage;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float turnSpeed = 0.5f;
    [SerializeField]
    private float runSpeed = 1f;

    private bool runBool = false , frisbeeInRange = false, holdingFrisbee, isEnabled;

    private FrisbeeThrower ft;

 


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        ft = GetComponent<FrisbeeThrower>();
        UiManager.Instance.m_GameEnd.AddListener(Disable);
        UiManager.Instance.m_OutOfBounds.AddListener(OutOfBound);
        holdingFrisbee = true;
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnabled)
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
            if (Input.GetKeyDown(KeyCode.R) && holdingFrisbee == false && frisbeeInRange)
            {
                animator.SetTrigger("Pick Up");
                holdingFrisbee = true;
            }

            //throw
            if (Input.GetKeyDown(KeyCode.T) && holdingFrisbee == true)
            {
                UiManager.Instance.increaseThrowCount();
                Debug.Log($"Start Throw{Time.time}");
                animator.SetTrigger("Throw");
                holdingFrisbee = false;
            }

            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed, 0);

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float speed = moveSpeed * Input.GetAxis("Vertical");

            if (!holdingFrisbee)
            {
                controller.SimpleMove(forward * speed * runSpeed);
                animator.SetFloat("Speed", speed);
            }

            UiManager.Instance.CageDistance = Mathf.RoundToInt(Vector3.Distance(ft.Frisbee.position, Cage.transform.position));
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Frisbee"))
        {
            frisbeeInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Frisbee"))
        {
            frisbeeInRange = false;
        }
    }

    private void Disable()
    {
        isEnabled = false;
        animator.SetTrigger("Endgame");
    }
    private void OutOfBound()
    {
        animator.SetTrigger("Pick Up");
        holdingFrisbee = true;
    }
}

