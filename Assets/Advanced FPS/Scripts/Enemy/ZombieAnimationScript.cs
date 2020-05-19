using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimationScript : MonoBehaviour
{
    public PlayerData playerData;
    private Animator animator;
    private NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        UpdateAnimation();
        UpdateRotateTowards(playerData.playerPos);
    }

    void UpdateAnimation()
    {
        float speed = Mathf.Lerp(0, Mathf.Pow(agent.speed, 2), agent.velocity.sqrMagnitude);
        animator.SetFloat("MoveSpeed", speed);
    }

    void UpdateRotateTowards(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion targetQua = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQua, Time.deltaTime);
    }
}
