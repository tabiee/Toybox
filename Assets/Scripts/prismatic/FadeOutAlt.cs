using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAlt : MonoBehaviour
{
    public float fadeSpeed = 5.0f;

    public float fadeDuration = 3.0f;
    public float fadeAllowed;

    public void FadeIn()
    {
        fadeAllowed = Time.time + fadeDuration;
    }
    private void Update()
    {
        //fade OUT otherwise fade IN
        if (Time.time > fadeAllowed)
        {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            fadeAmount = Mathf.Clamp(fadeAmount, 0.0f, 1.0f);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
        }
        else
        {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            fadeAmount = Mathf.Clamp(fadeAmount, 0.0f, 1.0f);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
        }
    }
}
