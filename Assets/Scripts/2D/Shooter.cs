using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject defaultBulletPrefab;

    //the shooter needs a prefab to modify. default bullet
    //the shooter should have WPattern and BType (that can be changed)
    //shooter uses that data to modify default data of the prefab
    //shooter shoots the shooty mc shoot thing

    protected void Shoot(ProjectileData projData)
    {
        Debug.Log($"{ this} says that bulletsPerShot is: {projData.weaponPattern.bulletsPerShot}");
    }

    //protected void FireBullet(Vector3 position, Vector3 direction, float speed)
    //{
    //    // Example implementation: Instantiate a bullet GameObject
    //    GameObject bullet = InstantiateBullet(position);
    //}

    //// Helper method to instantiate a bullet GameObject
    //private GameObject InstantiateBullet(Vector3 position)
    //{
    //    // Example: Instantiate a bullet prefab at the specified position
    //    GameObject bulletPrefab = GetBulletPrefab(); // Implement this method in subclasses
    //    GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
    //    return bullet;
    //}

    ////Abstract method to be implemented by subclasses to provide the bullet prefab
    //protected abstract GameObject GetBulletPrefab();
}
