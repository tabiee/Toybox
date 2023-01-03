using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject prefabSpawned;
    public float spawnSpeed = 1f;
    private float cooldown = 0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.P) && Time.time > cooldown)
        {
            cooldown = Time.time + spawnSpeed;
            Instantiate(prefabSpawned, transform.position, Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)));
        }
    }
}
