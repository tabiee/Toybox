using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private Transform holdArea;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float pickupRange = 3.5f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private float throwForce = 10.0f;

    private GameObject heldObj;
    private Rigidbody heldRB;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, ~layerMask))
                {
                    //pickup
                    Grab(hit.transform.gameObject);
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickupRange, Color.blue, 0.1f);
                }
            }
            else
            {
                //drop
                Drop();
            }
        }
        if (Input.GetMouseButtonDown(0) && heldObj != null)
        {
            //throw
            Throw();
        }
        if (heldObj != null)
        {
            //move
            Move();
        }
    }
    void Move()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = holdArea.position - heldObj.transform.position;
            heldRB.AddForce(moveDirection * pickupForce);
        }
    }
    void Grab(GameObject pickObj)
    {
        if (pickObj.gameObject.TryGetComponent(out Rigidbody rbody))
        {
            heldRB = rbody;
            heldRB.useGravity = false;
            heldRB.drag = 20;
            heldRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }
    void Drop()
    {
        heldRB.useGravity = true;
        heldRB.drag = 1;
        heldRB.constraints = RigidbodyConstraints.None;

        heldRB.transform.parent = null;
        heldObj = null;
    }
    void Throw()
    {
        heldRB.useGravity = true;
        heldRB.drag = 1;
        heldRB.constraints = RigidbodyConstraints.None;

        // throw
        heldRB.AddForce(transform.forward * throwForce, ForceMode.Impulse);

        heldRB.transform.parent = null;
        heldObj = null;
    }
}
