using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter
{
    public static PlayerShooter instance;
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
        Shoot(transform.rotation);
        Debug.Log("pew! I shot a bullet");
    }
}

//what i want
//shooter class handles how the bullet is shot
//projectile determines how the bullet behaves
//child class of shooter uses different logic for shooting
//playershooter cant do bullethell patterns
//enemyshooter can do bullethell patterns or normal shooting
//(do i need a shooter then?)
//playershooter and enemyshooter provide shooter and projectile with data on how they shoot the bullet
//eg. player shoots fast with small bullets -> Shooter: fireRate = 0.1f, Projectile: bulletSize = 0.25f

//for player
//wep: projspeed, firerate | bullet: size, damage, range