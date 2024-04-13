using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Type", menuName = "Projectile Assets/Create Bullet Type")]
public class BulletType : ScriptableObject
{
    public float projectileSpeed;
    //how fast the proj moves
    public float projectileRange = 1f;
    //how long until the bullet is destroyed
    public Vector3 projectileShape;
    //how big and/or what shape the bullet is
    public Sprite projectileSprite;
    //the sprite
    public Color projectileColor;
    //the color
    public int damageDealt;
    //how much dmg it does on hit

    //public EffectOnHit effectOnHit;
    //what it does when hitting an enemy/player
}
