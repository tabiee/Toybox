using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class PlayerController : MonoBehaviour
{
    [Header("Selected Objects")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private GameObject playerObject;
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 500.0f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [Space(10)]
    [SerializeField] private float sprintSpeed = 1200.0f;
    [SerializeField] private float sprintCooldown = 5.0f;
    [SerializeField] private int sprintDuration = 500;
    [Space(10)]
    [Tooltip("Higher crouchSpeed means you get slowed more.")]
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float jumpHeight = 10.0f;
    #region Private Variables
    private float crouchModifier = 1f;
    private float cooldownWaitTime = 0.0f;

    private float rotX;
    private float rotY;

    private Vector3 moveInput;

    private bool isGrounded = true;
    private bool hasJumped = false;

    private StarterAssetsInputs starterInputs;
    #endregion
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        starterInputs = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        CameraRotation();
        GetInput();
    }
    private void FixedUpdate()
    {
        ProcessMovement();
    }
    private void GetInput()
    {
        moveInput = new Vector3(starterInputs.move.x, 0.0f, starterInputs.move.y).normalized;
    }
    private void ProcessMovement()
    {
        Sprint();
        Jumping();
        Crouching();
    }
    private bool IsSprinting()
    {
        return starterInputs.sprint && sprintDuration > 2 && CooldownOver();
    }
    private bool CooldownOver()
    {
        return Time.time > cooldownWaitTime;
    }

    private void Sprint()
    {
        //if shift is held = run, else = walk, has duration of ~5s, cd of ~3s
        sprintDuration = Mathf.Clamp(sprintDuration, 0, 500);

        if (sprintDuration == 0)
        {
            cooldownWaitTime = Time.time + sprintCooldown;
        }

        float internalSpeed;

        if (IsSprinting())
        {
            sprintDuration -= 2;
            internalSpeed = sprintSpeed;
        }
        else
        {
            sprintDuration++;
            internalSpeed = moveSpeed;
        }

        Vector3 moveVector = transform.TransformDirection(moveInput) * internalSpeed / crouchModifier;
        rigidBody.velocity = new Vector3(moveVector.x, rigidBody.velocity.y, moveVector.z);
    }
    private bool CanJump()
    {
        return starterInputs.jump && isGrounded && !hasJumped;
    }
    private void Jumping()
    {
        if (!CanJump())
        {
            return;
        }

        rigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        hasJumped = true;
        isGrounded = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                isGrounded = true;
                break;
            }
            Debug.DrawRay(contact.point, contact.normal, Color.red);
        }

        if (isGrounded)
        {
            hasJumped = false; 
        }
    }
    private void Crouching()
    {
        //when crouch button is held, sets the crouchSpeed to the modifier that the movement speed is divided by
        if (starterInputs.crouch)
        {
            playerObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
            crouchModifier = crouchSpeed;
        }
        else
        {
            playerObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            crouchModifier = 1;
        }
    }
    private void CameraRotation()
    {
        if (starterInputs.look.sqrMagnitude >= 0.01f)
        {
            rotY += starterInputs.look.y * rotationSpeed * Time.deltaTime;
            rotX = starterInputs.look.x * rotationSpeed * Time.deltaTime;

            rotY = Mathf.Clamp(rotY, -90f, 90f);

            mainCamera.transform.localRotation = Quaternion.Euler(rotY, 0.0f, 0.0f);
            transform.Rotate(Vector3.up * rotX);
        }
        //move cam on top of player
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }
}