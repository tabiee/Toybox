using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    //this is a speaker
    //eventHandlers
    public event EventHandler<OnSpaceTestArgs> OnSpaceTest;
    public class OnSpaceTestArgs : EventArgs
    {
    //extra params for eventHandler
        public int spaceCount;
    }
    //custom delegates
    public event DelegateTest OnTap;
    public delegate void DelegateTest(float f);

    //actions, simplified shit
    public event Action<bool, int> OnAction;

    public UnityEvent OnUnityEvent;

    private int spaceCount;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceCount++;
            OnSpaceTest?.Invoke(OnSpaceTest, new OnSpaceTestArgs { spaceCount = spaceCount });

            OnTap?.Invoke(2.5f);

            OnAction?.Invoke(true, 3);

            OnUnityEvent?.Invoke();
        }
    }
}
