using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBar : MonoBehaviour
{
    // the hotbar becomes even hotter
    [Header("Color Settings")]
    public int color = 0;

    public Image scrollBarDefault;
    public Image scrollBar1;
    public Image scrollBar2;

    public int player = 6;
    public int reds = 7;
    public int yellows = 8;

    private void Awake()
    {
        var red = GameObject.FindGameObjectsWithTag("red");
        var yellow = GameObject.FindGameObjectsWithTag("yellow");

        foreach (GameObject reds in red)
        {
            Color redColor = reds.GetComponent<Renderer>().material.color;
            redColor = new Color(redColor.r, redColor.g, redColor.b, 0);
            reds.GetComponent<Renderer>().material.color = redColor;
        }
        foreach (GameObject yellows in yellow)
        {
            Color yellowColor = yellows.GetComponent<Renderer>().material.color;
            yellowColor = new Color(yellowColor.r, yellowColor.g, yellowColor.b, 0);
            yellows.GetComponent<Renderer>().material.color = yellowColor;
        }
    }
    private void Start()
    {
        // make colors not collide with each other
        Physics.IgnoreLayerCollision(reds, yellows);

    }
    void Update()
    {
        CheckColor();

        //scroll down to go to next bar
        if (Input.mouseScrollDelta.y == -1)
        {
            if (color < 2)
            {
                color++;
            }
            else
            {
                color = 0;
            }
        }
        //scroll up to go to previous bar
        else if (Input.mouseScrollDelta.y == 1)
        {
            if (color > 0)
            {
                color--;
            }
            else
            {
                color = 2;
            }
        }
        //reset bar to default
        if (Input.GetKeyDown(KeyCode.Q))
        {
            color = 0;
        }
    }
    void CheckColor()
    {
        //find all tagged color objects
        var red = GameObject.FindGameObjectsWithTag("red");
        var yellow = GameObject.FindGameObjectsWithTag("yellow");

        //default to none
        if (color == 0)
        {
            scrollBarDefault.enabled = true;
        }
        else
        {
            scrollBarDefault.enabled = false;
        }

        //if specific bar selected, activate it, else deactivate it
        if (color == 1)
        {
            scrollBar1.enabled = true;
            Physics.IgnoreLayerCollision(reds, player, false);

            foreach (GameObject reds in red)
            {
                reds.GetComponent<FadeOut>().StartFadeIn();

                if (reds.gameObject.TryGetComponent(out Rigidbody rbody))
                {
                    rbody.useGravity = true;
                }
            }
        }
        else
        {
            scrollBar1.enabled = false;
            Physics.IgnoreLayerCollision(reds, player);

            foreach (GameObject reds in red)
            {
                reds.GetComponent<FadeOut>().StartFadeOut();

                if (reds.gameObject.TryGetComponent(out Rigidbody rbody))
                {
                    rbody.useGravity = false;
                }
            }

        }

        if (color == 2)
        {
            scrollBar2.enabled = true;
            Physics.IgnoreLayerCollision(yellows, player, false);

            foreach (GameObject yellows in yellow)
            {
                yellows.GetComponent<FadeOut>().StartFadeIn();

                if (yellows.gameObject.TryGetComponent(out Rigidbody rbody))
                {
                    rbody.useGravity = true;
                }
            }
        }
        else
        {
            scrollBar2.enabled = false;
            Physics.IgnoreLayerCollision(yellows, player);

            foreach (GameObject yellows in yellow)
            {
                yellows.GetComponent<FadeOut>().StartFadeOut();

                if (yellows.gameObject.TryGetComponent(out Rigidbody rbody))
                {
                    rbody.useGravity = false;
                }
            }
        }
    }
}
