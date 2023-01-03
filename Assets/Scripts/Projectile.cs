using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Projectile : MonoBehaviour
{
    private Rigidbody proj;
    public GameObject explode;
    public int duration = 3;
    public float force = 3;
    public int damageDealt = 10;

    private bool hitOnce = false;
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
        if (collision.gameObject.tag == "Enemy" && hitOnce == false)
        {
            collision.gameObject.transform.Find("Trigger").GetComponent<Enemy>().HitBy(damageDealt);
            Debug.Log("Enemy hit!");
            hitOnce = true;
        }
        GameObject explosion = Instantiate(explode, this.transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
}
