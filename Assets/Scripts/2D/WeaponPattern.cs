using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Pattern", menuName = "Projectile Assets/Create Weapon Pattern")]
public class WeaponPattern : ScriptableObject
{
    public float projectileSpeed;
    public int bulletsPerShot;
    public float angleSpread;
    public float rateOfFire;
}
