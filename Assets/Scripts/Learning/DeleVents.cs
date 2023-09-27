using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleVents : MonoBehaviour
{
    //speakers and listeners

    public void Start()
    {
        //this is a listener that got subbed to the speaker event
        Events eventTest = GetComponent<Events>();
        eventTest.OnSpaceTest += OutsiderFunc;
        eventTest.OnTap += FuncToRun;
        eventTest.OnAction += ActiveSelf;
    }
    public void OutsiderFunc(object sender, Events.OnSpaceTestArgs e)
    {

        Debug.Log($"Outsider ran {e.spaceCount} time(s)");
    }
    public void FuncToRun(float f)
    {
        Debug.Log($"FuncToRun ran with float -{f}-!");
    }

    //no this isnt exactly like activeSelf, i was just joshing
    public void ActiveSelf(bool b, int i)
    {
        Debug.Log($"Self says {b} and {i}!");
    }

    public void UnityBased()
    {
        Debug.Log("UnityEvent listener!");
    }
}