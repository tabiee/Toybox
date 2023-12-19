using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listeners : MonoBehaviour
{
    //speakers and listeners

    public void Start()
    {
        //this is a listener that got subbed to the speaker/publisher events
        Publisher myEvents = GetComponent<Publisher>();
        myEvents.OnEvent += EventMethod;
        myEvents.OnDelegate += DelegateMethod;
        myEvents.OnAction += ActionMethod;
    }
    public void EventMethod(object sender, Publisher.OnEventArgs e)
    {
        Debug.Log($"EventHandler ran this {e.amount} time(s)!");
    }
    public void DelegateMethod(float f)
    {
        Debug.Log($"Delegate ran this with float: {f}!");
    }

    //no this isnt exactly like activeSelf, i was just joshing
    public void ActionMethod(bool b, int i)
    {
        Debug.Log($"Action ran this with {b} and {i}!");
    }

    public void UnityMethod()
    {
        Debug.Log("UnityEvent ran this!");
    }
}