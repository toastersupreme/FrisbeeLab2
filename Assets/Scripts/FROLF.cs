using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FROLF : MonoBehaviour
{

    public GameObject object1;
    public GameObject object2;
    public TextMeshProUGUI text1;

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);

        text1.text = distance.ToString();
    }
}
