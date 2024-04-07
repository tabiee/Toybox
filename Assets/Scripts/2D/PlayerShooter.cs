using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter
{
    public static PlayerShooter instance;
    public ProjectileData projectileData;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);

            Debug.LogWarning("There was more than one PlayerShooter in the scene");
        }
        instance = this;
    }
    public void FireBullet()
    {
        //asd
        Shoot(projectileData);
        Debug.Log("pew! I shot a bullet");
    }
}
public class PlayerStats
{

}
