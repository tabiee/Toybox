using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData projectileData;
    public Vector3 direction;
    private void OnCollisionEnter(Collision collision)
    {
        //ashdajskdjaskdasdksa
    }

    private void Start()
    {
        float projectileLifetime = projectileData.projectileRange;
        Invoke("DestroyThis", projectileLifetime);
    }
    private void Update()
    {
        MoveProjectile();
    }
    void MoveProjectile()
    {
        transform.Translate(direction.normalized * projectileData.projectileSpeed * Time.deltaTime);
    }
    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
