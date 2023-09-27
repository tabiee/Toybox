using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowMouse : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private Vector3 mousePosition;
    private bool isFollowing = true;

    void Update()
    {
        if (isFollowing)
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isFollowing = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            isFollowing = true;
        }
    }
    void Movement()
    {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }
}
