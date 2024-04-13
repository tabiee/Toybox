using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private ProjectileData projectileData;

    public bool isShooting;

    [SerializeField] private float startingDistance = 0.1f;
    private Projectile projectile;
    private Quaternion targetRotation;
    protected void Start()
    {
        projectile = projectilePrefab.GetComponent<Projectile>();
        projectile.projectileData = projectileData;
    }
    protected void Shoot(Quaternion targetRot)
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine(targetRot));
        }
    }
    private IEnumerator ShootRoutine(Quaternion targetRot)
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;
        GameObject bullet;

        GetProjectileAngle(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (projectileData.stagger) { 
            timeBetweenProjectiles = projectileData.timeBetweenBursts / projectileData.projectilesPerBurst; 
        }

        for (int i = 0; i < projectileData.burstCount; i++)
        {
            if (projectileData.oscillate) {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            targetRotation = targetRot;

            for (int j = 0; j < projectileData.projectilesPerBurst; j++)
            {

                Vector2 pos = FindProjectileSpawnLoc(currentAngle);
                bullet = Instantiate(projectilePrefab, pos, Quaternion.identity);
                bullet.transform.rotation = targetRotation;
                projectile.direction = bullet.transform.position - transform.position;
                currentAngle += angleStep;

                if (projectileData.stagger) {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }

            currentAngle = startAngle;

            if (!projectileData.stagger) {
                yield return new WaitForSeconds(projectileData.timeBetweenBursts);
            }
        }
        yield return new WaitForSeconds(projectileData.rateOfFire);
        isShooting = false;
    }


    private void GetProjectileAngle(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        float targetAngle = Mathf.Atan2(projectile.direction.y, projectile.direction.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;
        if (projectileData.angleSpread != 0)
        {
            angleStep = projectileData.angleSpread / (projectileData.projectilesPerBurst - 1);
            halfAngleSpread = projectileData.angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }
    private Vector2 FindProjectileSpawnLoc(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
