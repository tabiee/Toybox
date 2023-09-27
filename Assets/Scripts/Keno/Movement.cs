using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float verticalSpeed = 5f;
    //[SerializeField] private float decelSpeed = 2f;
    //[SerializeField] private float sinkingSpeed = 2f;

    private Rigidbody rb;
    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;
    //private float decelInit = 0f;
    //private float decelRate = 0f;
    //private Vector3 lastMovement;
    //private Vector3 targetDestination;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //ProcessMovement();
          ProcessRigidbody();
    }
    private void ProcessRigidbody()
    {
        //get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //calculate mouse rotations
        horizontalRotation += mouseX;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);

        //get keyboard movement
        float moveX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        //calculate keyboard movements
        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        rb.AddForce(movement, ForceMode.VelocityChange);

        // go up/down directly
        float moveY = 0f;
        if (Input.GetKey(KeyCode.Space))
        {
            moveY = verticalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            moveY = -verticalSpeed * Time.deltaTime;
        }
        rb.AddForce(Vector3.up * moveY, ForceMode.VelocityChange);
    }

    //private void ProcessMovement()
    //{
    //    //get mouse input
    //    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    //    //calculate mouse rotations
    //    horizontalRotation += mouseX;
    //    verticalRotation -= mouseY;
    //    verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

    //    transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);

    //    //get keyboard movement
    //    float moveX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
    //    float moveZ = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

    //    //calculate keyboard movements
    //    Vector3 movement = transform.right * moveX + transform.forward * moveZ;
    //    transform.localPosition += movement;

    //    //inertia
    //    if (movement.magnitude > 0f)
    //    {
    //        lastMovement = movement.normalized;
    //        targetDestination = transform.localPosition + lastMovement;
    //        decelInit += Time.deltaTime * 2;
    //        decelInit = Mathf.Clamp(decelInit, 0f, 6f);
    //        decelRate = decelInit;
    //    }
    //    else
    //    {
    //        decelInit = 0f;
    //    }

    //    if (targetDestination != Vector3.zero)
    //    {
    //        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetDestination, decelRate * Time.deltaTime);

    //        if (Vector3.Distance(transform.localPosition, targetDestination) < 0.1f)
    //        {
    //            targetDestination = transform.localPosition + lastMovement;
    //        }
    //    }
    //   decelRate -= decelSpeed * Time.deltaTime;
    //   decelRate = Mathf.Max(decelRate, 0f);


    //    // go up/down directly
    //    float moveY = 0f;
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        moveY = verticalSpeed * Time.deltaTime;
    //    }
    //    else if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        moveY = -verticalSpeed * Time.deltaTime;
    //    }
    //    //transform.position += transform.up * moveY;
    //    transform.Translate(Vector3.up * moveY, Space.World);

    //    // sinking
    //    //transform.position -= transform.up * sinkingSpeed * Time.deltaTime;
    //    transform.Translate(-Vector3.up * sinkingSpeed * Time.deltaTime, Space.World);
    //}
}
