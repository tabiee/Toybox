using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform enemy;
    public float knockback = 4f;
    public float knockup = 6f;
    public float knockdownTime = 2f;
    public float onMeshThreshold = 3f;
    public float kbCooldown = 1f;

    private NavMeshAgent enemyAI;
    private Rigidbody enemyRB;
    private PlayerControl playerScript;
    private Quaternion defaultRotation;

    private bool playerNearby;
    private float kbAllow = 0f;
    private void Start()
    {
        Physics.IgnoreLayerCollision(10, 10);
        transform.rotation = transform.parent.rotation;

        //grab needed components and find the player
        enemy = transform.parent;
        enemyAI = enemy.transform.GetComponent<NavMeshAgent>();
        enemyRB = enemy.transform.GetComponent<Rigidbody>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerControl>();

        //if theres no player give error
        if (playerScript == null)
            Debug.LogError("Player script is Null");

        defaultRotation = transform.rotation;
    }
    private void Update()
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
            Invoke("ResetAI", knockdownTime);
        }
    }
    void ResetAI()
    {
        enemyAI.enabled = true;
        enemyRB.isKinematic = true;

    }
    public void HitBy()
    {
        Debug.Log("Hitby triggered!");

        enemy.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        enemy.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        enemy.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * knockback, ForceMode.Impulse);
        enemy.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * knockup, ForceMode.Impulse);

        kbAllow = Time.time + kbCooldown;
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
}
