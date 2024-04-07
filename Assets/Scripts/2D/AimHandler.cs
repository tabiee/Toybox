using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimHandler : MonoBehaviour
{
    public static AimHandler instance;

    [SerializeField] private float rotationSpeed = 15f;

    private bool isHeld;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);

            Debug.LogWarning("There was more than one AimHandler in the scene");
        }
        instance = this;
    }
    private void Update()
    {
        HandleAiming();
        Shoot();
    }
    private void HandleAiming()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnShoot(InputValue inputValue)
    {
        isHeld = inputValue.isPressed;
    }
    void Shoot()
    {
        if (isHeld)
        {
            PlayerShooter.instance.FireBullet();
            Debug.Log("Pew!");
        }
    }
}
