using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] float g = 1f;
    static float G;

    public static List<Rigidbody2D> attractors = new List<Rigidbody2D>();
    public static List<Rigidbody2D> attractees = new List<Rigidbody2D>();
    private void FixedUpdate()
    {
        G = g; // for editor
        DoGravity();
    }
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
        //this uses newtons gravity F = G * (m1 * m2) / r^2
        //i dont fully understand the formula but the code makes sense, even if i wouldnt be able to come up with it on my own

        //m1 * m2 * G
        float massProduct = attractor.mass * target.mass * G;

        //get direction between objects n stuff
        Vector3 difference = attractor.position - target.position;
        float distance = difference.magnitude;

        //this is all of above divided by r^2, or i guess r * r but same shit
        float unScaledforceMagnitude = massProduct / distance * distance;
        float forceMagnitude = G * unScaledforceMagnitude;

        //actual direction & vector magic
        Vector3 forceDirection = difference.normalized;
        Vector3 forceVector = forceDirection * forceMagnitude;
        target.AddForce(forceVector);
    }
}
