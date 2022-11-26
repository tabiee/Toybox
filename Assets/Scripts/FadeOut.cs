using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float fadeSpeed = 3.0f;

    public bool waitOut, waitIn = true;
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutObject());
    }
    public void StartFadeIn()
    {
        StartCoroutine(FadeInObject());
    }
    public IEnumerator FadeOutObject()
    {
        //if this is running, don't do anything
        //ColorBar continues requesting the fades until the condition passes
        //thus making it run only once and only when both are finished
        if (waitOut == true && waitIn == true)
        {
            while (this.GetComponent<Renderer>().material.color.a > 0)
            {
                waitOut = false;
                Color objectColor = this.GetComponent<Renderer>().material.color;
                float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                fadeAmount = Mathf.Clamp(fadeAmount, 0.0f, 1.0f);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                this.GetComponent<Renderer>().material.color = objectColor;

                //Debug.Log("ALPHA: " + objectColor.a);
                //Debug.Log(fadeAmount);
                yield return null;
                waitOut = true;
            }
        }
    }
    public IEnumerator FadeInObject()
    {
        if (waitOut == true && waitIn == true)
        {
            while (this.GetComponent<Renderer>().material.color.a < 1)
            {
                waitIn = false;
                Color objectColor = this.GetComponent<Renderer>().material.color;
                float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                fadeAmount = Mathf.Clamp(fadeAmount, 0.0f, 1.0f);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                this.GetComponent<Renderer>().material.color = objectColor;

                //Debug.Log("ALPHA: " + objectColor.a);
                //Debug.Log(fadeAmount);
                yield return null;
                waitIn = true;
            }
        }
    }
}
