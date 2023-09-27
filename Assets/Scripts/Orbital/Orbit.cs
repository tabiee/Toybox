using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private Transform gravitatedTo;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float maxGravity;
    [SerializeField] private float maxGravityDist;

    private float lookAngle;
    private Vector3 lookDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravitatedTo = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
            DoGravity();
    }

    void DoGravity()
    {
        float dist = Vector2.Distance(gravitatedTo.transform.position, transform.position);

        Vector3 v = gravitatedTo.transform.position - transform.position;
        rb.AddForce(v.normalized * (1.0f - dist / maxGravityDist) * maxGravity);

        lookDirection = gravitatedTo.transform.position - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }
}
