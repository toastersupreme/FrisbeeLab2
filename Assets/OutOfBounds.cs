using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Frisbee"))
        {
            //reset the frisbee
            UiManager.Instance.m_OutOfBounds.Invoke();
        }
    }
}
