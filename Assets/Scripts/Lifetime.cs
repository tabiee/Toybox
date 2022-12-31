using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Lifetime : MonoBehaviour
{
    private Rigidbody proj;
    public GameObject explode;
    public int duration = 3;
    public float force = 3;
    private void Start()
    {
        proj = GetComponent<Rigidbody>();
        proj.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
    }
    void Update()
    {
        Destroy(gameObject, duration);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.transform.Find("Trigger").GetComponent<Enemy>().HitBy();
            Debug.Log("Enemy hit!");
        }
        GameObject explosion = Instantiate(explode, this.transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
}
