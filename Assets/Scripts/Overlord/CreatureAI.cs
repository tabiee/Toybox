using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Reflection;

public class CreatureAI : MonoBehaviour
{
    [SerializeField] private float wanderInterval = 3f;
    [SerializeField] private float wanderDistance = 5f;
    [SerializeField] private GameObject particlePrefab;

    private NavMeshAgent agent;
    private Rigidbody creatureRigidbody;
    private Vector3 destination;
    private bool isWandering = false;
    private bool hasRunOnce = false;

    public float onMeshThreshold = 3f;
    public bool isPickedUp = true;

    private float resetCooldown = 0.5f;
    private float cooldownWaitTime = 0f;
    private bool resetOnce = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        creatureRigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
    //if pickedUp, drop and act like a wet noodle, else get back up
    //also check if yer on the navmesh
        if (isPickedUp)
        {
            if (IsAgentOnNavMesh() && CooldownOver() && creatureRigidbody.velocity.y <= 0)
            {
                cooldownWaitTime = resetCooldown + Time.time;
                if (resetOnce == false)
                {
                    Invoke("ResetAI", resetCooldown);
                    resetOnce = true;
                }
            }

            agent.velocity = Vector3.zero;
            agent.enabled = false;
        }

        else
        {
            agent.enabled = true;
            Invoke("StartWander", Random.Range(0f, wanderInterval));
        }
    }
    private bool CooldownOver()
    {
        return Time.time > cooldownWaitTime;
    }
    private void ResetAI()
    {
        isPickedUp = false;
        resetOnce = false;
    }
    public bool IsAgentOnNavMesh()
    {
        Vector3 agentPosition = transform.position;
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
    private void StartWander()
{
            if (isWandering || isPickedUp) return;

            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            Vector3 targetPosition = transform.position + randomDirection * wanderDistance;

            if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, wanderDistance, NavMesh.AllAreas))
            {
                destination = hit.position;
                agent.SetDestination(destination);
                isWandering = true;

                Invoke("StopWander", Random.Range(0f, wanderInterval));
            }
    }
    private void StopWander()
    {
        isWandering = false;

        Invoke("StartWander", Random.Range(0f, wanderInterval));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isPickedUp && !hasRunOnce)
        {
            hasRunOnce = true;
            AddCreatureByTag();

            GameObject collectionEffect = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
            Destroy(collectionEffect, 2f);
            Destroy(gameObject);
        }
    }
    void AddCreatureByTag()
    {
        string objectTag = gameObject.tag;
        int creatureValue = LevelData.GetCreatureValue(objectTag);

        creatureValue++;
        LevelData.SetCreatureValue(objectTag, creatureValue);
    }
}
