using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public float moving = 15f;
    public float turning = 5f;
    public float jumping = 25f;
    private Rigidbody rb;

    private bool isGrounded = true;
    private bool hasJumped = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint c in collision.contacts)
        {
            if (Vector3.Dot(c.normal, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z)) > 0.5f)
            {
                isGrounded = true;
            }
            Debug.DrawRay(c.point, c.normal, Color.red);
        }
    }
    private void FixedUpdate()
    {
        //accelerate
        float vt = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(0f, 0f, vt * moving);
        rb.AddRelativeForce(force);

        //turn
        float hz = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0f, hz * turning, 0f) * Time.deltaTime);

        //jump
        if (Input.GetKey(KeyCode.Space) && isGrounded == true && hasJumped == false)
        {
            rb.AddRelativeForce(Vector3.up * jumping, ForceMode.Impulse);
            hasJumped = true;
            isGrounded = false;
        }

        //reset jump
        if (isGrounded == true)
        {
            Debug.Log("Marked hasJumped as false");
            hasJumped = false;
        }
    }
}
