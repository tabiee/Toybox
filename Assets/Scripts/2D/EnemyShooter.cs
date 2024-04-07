using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Shooter
{
    public ProjectileData projectileData;
    public void FireBullet()
    {
        //asd
        Shoot(projectileData);
        Debug.Log("pew! I shot a bullet");
    }
}
