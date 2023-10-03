using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is all tutorial
//https://www.youtube.com/watch?v=o3rt2FaMwBs

[RequireComponent(typeof(Rigidbody2D))]

public class Graviton : MonoBehaviour
{
    Rigidbody2D rb;
    public bool IsAttractor//stuff
    {
        get
        {
            return isAttractor;
        }
        set
        {
            //if object is marked as attractor, add it to the handler script's list of attractors, else remove it from there
            if (value == true)
            {
                if (!Orbit.attractors.Contains(this.GetComponent<Rigidbody2D>()))
                {
                    Orbit.attractors.Add(rb);
                }
            }
            else if (value == false)
            {
                Orbit.attractors.Remove(rb);
            }
            isAttractor = value;
        }
    }
    public bool IsAttractee//stuff
    {
        get
        {
            return isAttractee;
        }
        set
        {
            if (value == true)
            {
                if (!Orbit.attractees.Contains(this.GetComponent<Rigidbody2D>()))
                {
                    Orbit.attractees.Add(rb);
                }
            }
            else if (value == false)
            {
                Orbit.attractees.Remove(rb);
            }
            isAttractee = value;
        }
    }

    [SerializeField] bool isAttractor;
    [SerializeField] bool isAttractee;

    [SerializeField] Vector3 initialVelocity;
    [SerializeField] bool applyInitialVelocityOnStart;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        //when object is enabled, check how it interacts with gravity
        IsAttractor = isAttractor;
        IsAttractee = isAttractee;
    }
    private void Start()
    {
        if (applyInitialVelocityOnStart)
        {
            ApplyVelocity(initialVelocity);
        }
    }
    private void OnDisable()
    {
        Orbit.attractors.Remove(rb);
        Orbit.attractees.Remove(rb);
    }

    void ApplyVelocity(Vector3 velocity)
    {
        rb.AddForce(initialVelocity, ForceMode2D.Impulse);
    }
}
