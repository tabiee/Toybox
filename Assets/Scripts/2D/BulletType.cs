using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Type", menuName = "Projectile Assets/Create Bullet Type")]
public class BulletType : ScriptableObject
{
    public float bulletShape;
    public Color bulletColor;
    public int damageDealt;
    //public EffectOnHit effectOnHit;
}
