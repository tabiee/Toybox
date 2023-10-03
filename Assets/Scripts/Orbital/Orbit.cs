using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    //[SerializeField] private Transform gravitatedTo;
    //[SerializeField] private Rigidbody2D rb;

    //[SerializeField] private float maxGravity;
    //[SerializeField] private float maxGravityDist;

    //private float lookAngle;
    //private Vector3 lookDirection;

    [SerializeField] float g = 1f;
    static float G;

    public static List<Rigidbody2D> attractors = new List<Rigidbody2D>();
    public static List<Rigidbody2D> attractees = new List<Rigidbody2D>();

    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    gravitatedTo = GameObject.FindGameObjectWithTag("Player").transform;

    //}
    private void FixedUpdate()
    {
        G = g; // for editor
        DoGravity();
    }

    //void DoGravity()
    //{
    //    float dist = Vector2.Distance(gravitatedTo.transform.position, transform.position);

    //    Vector3 v = gravitatedTo.transform.position - transform.position;
    //    rb.AddForce(v.normalized * (1.0f - dist / maxGravityDist) * maxGravity);

    //    lookDirection = gravitatedTo.transform.position - transform.position;
    //    lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

    //    transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    //}

    public static void DoGravity()
    {
        foreach (Rigidbody2D attractor in attractors)
        {
            foreach (Rigidbody2D attractee in attractees)
            {
                if (attractor != attractee)
                    AddGravityForce(attractor, attractee);
            }
        }
    }

    public static void AddGravityForce(Rigidbody2D attractor, Rigidbody2D target)
    {
        float massProduct = attractor.mass * target.mass * G;

        Vector3 difference = attractor.position - target.position;
        float distance = difference.magnitude;

        float unScaledforceMagnitude = massProduct / distance * distance;
        float forceMagnitude = G * unScaledforceMagnitude;

        Vector3 forceDirection = difference.normalized;
        Vector3 forceVector = forceDirection * forceMagnitude;
        target.AddForce(forceVector);
    }
}
