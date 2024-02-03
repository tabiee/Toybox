using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private Vector3 heldLocation = new Vector3(0f, 0f, 2.5f);
    [SerializeField] private LayerMask layerToIgnore;
    [SerializeField] private float pickupRange = 3.5f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private float throwForce = 10.0f;

    private Transform heldAtEmpty;
    private GameObject heldObject;
    private Rigidbody heldRigidbody;

    private void Start()
    {
        //makes an empty object parented to this object to set that as the location where the picked up object sits at
        GameObject emptyObject = new GameObject("Hands");
        emptyObject.transform.parent = transform;
        emptyObject.transform.localPosition = heldLocation;
        heldAtEmpty = emptyObject.transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //E to pickup or drop
            ProcessObject();
        }

        if (Input.GetMouseButtonDown(0) && heldObject != null)
        {
            //left click to throw
            ThrowHeldObject();
        }

        if (heldObject != null)
        {
            //keep object infront of you
            PullHeldObject();
        }
    }
    void ProcessObject()
    {
        if (heldObject == null)
        {
            //raycast and pickup hit object
            CheckForObject();
        }
        else
        {
            //drop whatever you're holding
            DropHeldObject();
        }
    }
    void CheckForObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, ~layerToIgnore))
        {
            //pickup whats infront of you
            GrabObject(hit.transform.gameObject);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickupRange, Color.blue, 0.1f);
        }
    }
    void PullHeldObject()
    {
        if (Vector3.Distance(heldObject.transform.position, heldAtEmpty.position) > 0.1f)
        {
            Vector3 pullDirection = heldAtEmpty.position - heldObject.transform.position;
            heldRigidbody.AddForce(pullDirection * pickupForce);
        }
    }
    void GrabObject(GameObject pickedUpObject)
    {
        if (pickedUpObject.gameObject.TryGetComponent(out Rigidbody rb))
        {
            heldRigidbody = rb;
            heldRigidbody.useGravity = false;
            heldRigidbody.drag = 20;
            heldRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            heldRigidbody.transform.parent = heldAtEmpty;
            heldObject = pickedUpObject;
        }
    }
    void DropHeldObject()
    {
        heldRigidbody.useGravity = true;
        heldRigidbody.drag = 1;
        heldRigidbody.constraints = RigidbodyConstraints.None;

        heldRigidbody.transform.parent = null;
        heldObject = null;
    }
    void ThrowHeldObject()
    {
        heldRigidbody.useGravity = true;
        heldRigidbody.drag = 1;
        heldRigidbody.constraints = RigidbodyConstraints.None;

        // throw
        heldRigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);

        heldRigidbody.transform.parent = null;
        heldObject = null;
    }
}
