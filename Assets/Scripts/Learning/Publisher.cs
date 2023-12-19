using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Publisher : MonoBehaviour
{
    //these are speakers/publishers

    //eventHandlers
    public event EventHandler<OnEventArgs> OnEvent;
    public class OnEventArgs : EventArgs
    {
    //extra params for eventHandler
        public int amount;
    }

    //custom delegates
    public delegate void Delegate(float f);
    //--encapsulation, cant edit this delegate
    public event Delegate OnDelegate;
    //--unrestricted, you can modify this guy
    public Delegate CallDelegate;


    //actions, simplified shit
    public event Action<bool, int> OnAction;

    //from inspector
    public UnityEvent OnUnityEvent;

    private int spacePressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed++;
            OnEvent?.Invoke(OnEvent, new OnEventArgs { amount = spacePressed });

            OnDelegate?.Invoke(2.5f);

            OnAction?.Invoke(true, 3);

            OnUnityEvent?.Invoke();
        }
    }
}
