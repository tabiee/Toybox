using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    public Transform GameObject;
    void Update()
    {
       
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (GameObject.eulerAngles.x <= 180f)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }

    }
}
