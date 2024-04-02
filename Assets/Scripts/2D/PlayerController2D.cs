using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

    private Vector2 mousePosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
    private void OnAim(InputValue currentMousePos)
    {
        mousePosition = currentMousePos.Get<Vector2>();
    }
    private void FixedUpdate()
    {
        SetVelocity();
        SetRotation();
    }
    private void SetVelocity()
    {
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);

        rb.velocity = smoothedMovementInput * movementSpeed;
    }
    private void SetRotation()
    {
        //if (movementInput != Vector2.zero)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovementInput);
        //    Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //    rb.MoveRotation(rotation);
        //}

        //Plane plane = new Plane(Vector3.forward, Vector3.zero);
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 direction;
        //float enter;

        //if (plane.Raycast(ray, out enter))
        //{
        //    direction = ray.GetPoint(enter);

        //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        //}

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePos - transform.position;

        transform.up = direction;
    }
}