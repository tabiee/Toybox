using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gravity : MonoBehaviour
{
    [Range(-20, 10)] public float gravityValue = -10f;
    //public TMP_Text gravityText;
    void Update()
    {
        Physics.gravity = new Vector3(0f, gravityValue, 0f);
        //gravityText.text = "Gravity: \n" + gravityValue;

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            gravityValue++;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            gravityValue--;
        }
    }
}
