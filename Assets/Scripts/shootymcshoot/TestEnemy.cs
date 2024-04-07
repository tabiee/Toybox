using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TestEnemy : MonoBehaviour
{
    [Header("General")]
    public Transform enemy;
    public float knockback = 4f;
    public float knockup = 6f;
    public float knockdownTime = 2f;
    public float onMeshThreshold = 3f;
    public float kbCooldown = 1f;
    [Header("HP & DMG")]
    public int maxHealth = 50;
    public int currHealth;
    public int damageDealt = 5;
    public GameObject healthBarUI;
    public Slider hpSlider;

    public float dmgCooldown = 2f;
    private float dmgAllow = 0f;

    private NavMeshAgent enemyAI;
    private Rigidbody enemyRB;
    private GameObject playerObject;
    private PlayerControl playerScript;
    private TestPlayerStats playerStats;
    private Quaternion defaultRotation;

    private bool playerNearby;
    private float kbAllow = 0f;
    private bool resetOnce = false;
    private void Start()
    {
        //grab needed components and find the player
        enemy = transform.parent;
        enemyAI = enemy.transform.GetComponent<NavMeshAgent>();
        enemyRB = enemy.transform.GetComponent<Rigidbody>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponentInChildren<PlayerControl>();
        playerStats = playerObject.GetComponentInChildren<TestPlayerStats>();

        //random colors
        var enemyColor = enemy.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        enemy.gameObject.transform.Find("Backpack").GetComponent<Renderer>().material.color = enemyColor;
        enemy.gameObject.transform.Find("Leggy").GetComponent<Renderer>().material.color = enemyColor;
        enemy.gameObject.transform.Find("Leggy2").GetComponent<Renderer>().material.color = enemyColor;

        //no collision between enemies + fix rotation
        //Physics.IgnoreLayerCollision(10, 10);
        transform.rotation = transform.parent.rotation;
        defaultRotation = transform.rotation;

        //set health stuff
        currHealth = maxHealth;
        hpSlider.value = CalculateHealth();
        healthBarUI.GetComponent<Canvas>().worldCamera = Camera.main;

    }
    private void Update()
    {
        RotateEnemy();
        DealDamage();

        hpSlider.value = CalculateHealth();
        if (currHealth < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
    }
    void RotateEnemy()
    {
        //if player is near, face towards and set player location as destination to move towards
        //else reset to original rotation and set destination to current location aka stop moving
        if (enemyAI.enabled == true)
        {
            if (playerNearby == true)
            {
                enemyAI.destination = playerScript.transform.position;
                var targetRotation = Quaternion.LookRotation(playerScript.transform.position - transform.position);
                enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, targetRotation, 3.0f * Time.deltaTime);
            }
            else
            {
                enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, defaultRotation, 3.0f * Time.deltaTime);
                enemyAI.SetDestination(gameObject.transform.position);
            }
        }
        else if (enemyAI.enabled == false && enemyRB.velocity.y <= 0 && Time.time > kbAllow && IsAgentOnNavMesh() == true)
        {
            if (resetOnce == false)
            {
                Invoke("ResetAI", knockdownTime);
                resetOnce = true;
            }
        }
    }
    float CalculateHealth()
    {
        float currHp = currHealth;
        float maxHp = maxHealth;
        return currHp / maxHp;
    }
    void ResetAI()
    {
        enemyAI.enabled = true;
        enemyRB.isKinematic = true;
        currHealth = maxHealth;

        resetOnce = false;
    }
    public void HitBy(int damageTaken)
    {
        Debug.Log("Hitby triggered!");

        if (currHealth - damageTaken > 0)
        {
            currHealth -= damageTaken;
            Debug.Log(gameObject.transform.parent.name + " Enemy damaged! " + currHealth + " Health left!");
        }
        else if (enemyAI.enabled == true)
        {
            currHealth -= damageTaken;
            Debug.Log("Enemy knocked out!");
            enemyAI.enabled = false;
            enemyRB.isKinematic = false;
            enemyRB.AddRelativeForce(Vector3.back * knockback, ForceMode.Impulse);
            enemyRB.AddRelativeForce(Vector3.up * knockup, ForceMode.Impulse);

            kbAllow = Time.time + kbCooldown;
        }
    }
    public bool IsAgentOnNavMesh()
    {
        Vector3 agentPosition = enemy.transform.position;
        NavMeshHit hit;

        // Check for nearest point on navmesh to agent, within onMeshThreshold
        if (NavMesh.SamplePosition(agentPosition, out hit, onMeshThreshold, NavMesh.AllAreas))
        {
            // Check if the positions are vertically aligned
            if (Mathf.Approximately(agentPosition.x, hit.position.x)
                && Mathf.Approximately(agentPosition.z, hit.position.z))
            {
                // Lastly, check if object is below navmesh
                return agentPosition.y >= hit.position.y;
            }
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNearby = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNearby = false;
        }
    }

    private void DealDamage()
    {
        float distance = Vector3.Distance(playerScript.transform.position, enemy.transform.position);
        print("Distance between " + gameObject.transform.parent.name + " and the Player is: " + distance);

        if (distance < 5 && Time.time > dmgAllow)
        {
            print("Within range! Dealing damage.");
            playerStats.TakeDamage(enemy.gameObject, damageDealt);
            dmgAllow = Time.time + dmgCooldown;
        }
    }
}
