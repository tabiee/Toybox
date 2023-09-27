using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currHealth;
    public int knockback = 15;
    public int knockup = 5;

    private Rigidbody rb;

    private void Start()
    {
        currHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log("Current Player HP is: " + currHealth);
        if (currHealth == 0)
        {
            Debug.Log("Player is dead!");
        }
    }

    public void TakeDamage(GameObject enemyObject, int damageTaken)
    {
        currHealth -= damageTaken;
        var kbDirection = rb.transform.position - enemyObject.transform.position;

        rb.AddForce(kbDirection.normalized * knockback, ForceMode.Impulse);
        rb.AddForce(Vector3.up * knockup, ForceMode.Impulse);
    }
}
