using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rot : MonoBehaviour
{
    public Transform rotation;
    public Vector3 rotationDirectional;
    [SerializeReference] private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotationDirectional * rotationSpeed);
    }
}
