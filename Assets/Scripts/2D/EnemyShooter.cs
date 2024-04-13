using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Shooter
{
    private Transform targetPosition;
    new private void Start()
    {
        base.Start();
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        FireBullet();
    }
    public void FireBullet()
    {
        Shoot(GetTargetPosition());
        Debug.Log("pew! I shot a bullet");
    }

    private Quaternion GetTargetPosition()
    {
        Quaternion targetRotation;

        Vector3 dir = (targetPosition.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Debug.Log("tarRot: " + targetRotation);
        return targetRotation;
    }
}
