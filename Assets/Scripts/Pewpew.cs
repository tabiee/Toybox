using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pewpew : MonoBehaviour
{
    public GameObject projectile;
    public float spawnSpeed = 0.15f;
    public Transform spawnPoint;

    private float cooldown = 0.0f;
    private void Awake()
    {
        //spawnPoint = this.gameObject.transform;
    }
    void Shoot()
    {
        cooldown = Time.time + spawnSpeed;
        Instantiate(projectile, spawnPoint.position + (transform.forward * 1.25f), spawnPoint.transform.rotation);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && Time.time > cooldown)
        {
            Shoot();
        }
    }
}
