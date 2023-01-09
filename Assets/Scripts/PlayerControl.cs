using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Selected Objects")]
    public Transform cam;
    public Rigidbody rb;
    public GameObject body;
    public LayerMask notGround;

    [Header("Movement Settings")]
    public float moveSpeed = 4.0f;
    public float turnSpeed = 4.0f;
    public float sprintSpeed = 8.0f;
    public float sprintCooldown = 5.0f;
    public float jumpHeight = 10.0f;
    public int crouchModifier = 2;

    private int crouchSpeed = 1;
    private int sprintDuration = 500;
    private float sprintAllowed = 0.0f;

    private float rotX;
    private float rotY;

    private Vector3 moveInput;

    public bool isGrounded = true;
    public bool hasJumped = false;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        MovePlayer();
        TurnPlayer();
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint c in collision.contacts)
        {
            if (Vector3.Dot(c.normal, Vector3.up) > 0.5f)
            {
                isGrounded = true;
            }
            Debug.DrawRay(c.point, c.normal, Color.red);
        }
    }
    private void MovePlayer()
    {
        //get input
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        sprintDuration = Mathf.Clamp(sprintDuration, 0, 500);

        //if shift is held = run, else = walk, has duration of ~5s, cd of ~3s
        if (Input.GetKey(KeyCode.LeftShift) && sprintDuration > 2 && Time.time > sprintAllowed)
        {
            sprintDuration -= 2;

            Vector3 MoveVector = transform.TransformDirection(moveInput) * sprintSpeed / crouchSpeed;
            rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);
        }
        else
        {
            sprintDuration++;

            Vector3 MoveVector = transform.TransformDirection(moveInput) * moveSpeed / crouchSpeed;
            rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);
        }

        //sprint cd
        if (sprintDuration <= 2)
        {
            sprintAllowed = Time.time + sprintCooldown;
        }

        //reset jump
        if (isGrounded == true)
        {
            Debug.Log("Marked hasJumped as false");
            hasJumped = false;
        }

        //jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded == true && hasJumped == false)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            hasJumped = true;
            isGrounded = false;
        }

        //crouching
        if (Input.GetKey(KeyCode.LeftControl))
        {
            body.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
            crouchSpeed = crouchModifier;
        }
        else
        {
            body.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            crouchSpeed = 1;
        }
    }
    private void TurnPlayer()
    {
        rotY = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        rotX = Mathf.Clamp(rotX, -90f, 90f);

        cam.transform.eulerAngles = new Vector3(-rotX, cam.transform.eulerAngles.y + rotY, 0);
        transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y + rotY, 0);
    }
}
