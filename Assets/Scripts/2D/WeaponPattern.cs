using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Pattern", menuName = "Projectile Assets/Create Weapon Pattern")]
public class WeaponPattern : ScriptableObject
{
    public float rateOfFire;
    //time between each burst bullet in coroutine

    //note: if there's more than 1 projectile, either increase angleSpread or turn on stagger

    public int projectilesPerBurst = 1;
    //how many projectiles get instantiated in coroutine
    [Range(0, 359)] public float angleSpread;
    //how big the cone of bullet spread is

    //note: prefered to be only on enemies
    //note: if burstCount is 1, keep timeBetweenBursts at 0

    public int burstCount = 1;
    //how many bursts per cooldown
    public float timeBetweenBursts = 0f;
    //cooldown between each burst

    public bool stagger;
    //delay between each bullet in a burst
    public bool oscillate;
    //back and forth motion affected by angleSpread
}
