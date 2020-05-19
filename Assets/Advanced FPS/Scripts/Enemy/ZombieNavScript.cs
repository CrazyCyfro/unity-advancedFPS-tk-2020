using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieNavScript : MonoBehaviour
{
    public PlayerData playerData;
    private const float updateInterval = 0.1f;
    private NavMeshAgent agent;
    private IEnumerator followCoroutine;
    private ZombieAttackScript attackScript;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        followCoroutine = FollowPlayer();

        attackScript = GetComponent<ZombieAttackScript>();
    }

    void Start()
    {
        StartCoroutine(followCoroutine);
    }

    void OnDisable()
    {
        StopCoroutine(followCoroutine);
        agent.isStopped = true;
    }

    
    IEnumerator FollowPlayer()
    {
        while (agent.enabled) {
            if (attackScript.Attacking()) {
                agent.isStopped = true;
            } else {
                agent.isStopped = false;
            }
            
            agent.SetDestination(playerData.playerPos);
            yield return new WaitForSeconds(updateInterval);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        agent.velocity = Vector3.zero;
    }
}
