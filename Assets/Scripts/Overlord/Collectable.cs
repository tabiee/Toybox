using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Reflection;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float wanderInterval = 3f;
    [SerializeField] private float wanderDistance = 5f;
    [SerializeField] private GameObject particle;

    private NavMeshAgent agent;
    private Rigidbody creatureRB;
    private Vector3 destination;
    private bool isWandering = false;
    private bool once = false;

    public float onMeshThreshold = 3f;
    public bool isPickedUp = true;

    private float resetCD = 0.5f;
    private float resetAllow = 0f;
    private bool resetOnce = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        creatureRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
    //if pickedUp, drop and act like a wet noodle, else get back up
    //also check if yer on the navmesh
        if (isPickedUp)
        {
            if (IsAgentOnNavMesh() && Time.time > resetAllow && creatureRB.velocity.y <= 0)
            {
                resetAllow = resetCD + Time.time;
                if (resetOnce == false)
                {
                    Invoke("ResetAI", resetCD);
                    resetOnce = true;
                }
            }

            //agent.isStopped = true;
            //agent.updatePosition = false;
            //agent.updateRotation = false;
            agent.velocity = Vector3.zero;
            agent.enabled = false;
        }

        else
        {
            agent.enabled = true;
            //agent.isStopped = false;
            //agent.updatePosition = true;
            //agent.updateRotation = true;
            Invoke("StartWander", Random.Range(0f, wanderInterval));
        }
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
        if (!isPickedUp)
        {
            if (isWandering) return;

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
    }
    private void StopWander()
    {
        isWandering = false;

        Invoke("StartWander", Random.Range(0f, wanderInterval));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isPickedUp && !once)
        {
            once = true;
            AddCreatureByTag();

            GameObject explosion = Instantiate(particle, this.transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
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
