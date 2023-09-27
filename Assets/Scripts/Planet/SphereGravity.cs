using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGravity : MonoBehaviour
{
    public float sGravity = 9.81f;
    public Transform gravitatedTo;
    public Rigidbody rb;

    public bool autoOrient = true;
    public float autoOrientSpeed = 1f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        ApplyGravity();
    }
    void ApplyGravity()
    {
        Vector3 diff = transform.position - gravitatedTo.position;
        rb.AddForce(-diff.normalized * sGravity * rb.mass);
        Debug.DrawRay(transform.position, diff.normalized, Color.cyan);

        if (autoOrient == true)
        {
            AutoOrient(-diff);
        }
    }
    void AutoOrient(Vector3 down)
    {
        Quaternion orientationDIrection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDIrection, autoOrientSpeed * Time.deltaTime);
    }
}
