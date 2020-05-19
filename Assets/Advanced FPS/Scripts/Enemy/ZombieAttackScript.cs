using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackScript : MonoBehaviour
{
    public float attackSpd;
    public int damage;
    public LayerMask playerLayer;
    public PlayerData playerData;
    private const float attackDelay = 0.4f;
    private const float attackRecover = 1.35f;
    private Animator animator;
    private NavMeshAgent agent;
    private Transform source;

    private bool attacking;
    private Vector3 direction;
    
    void Awake()
    {
        if (attackSpd == 0) attackSpd = 1;
        agent = GetComponent<NavMeshAgent>();
        source = transform.Find("Raycast Source");
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (attacking) return;
        if (agent.pathStatus != NavMeshPathStatus.PathComplete) return;
        if (agent.velocity != Vector3.zero) return;

        direction = playerData.playerPos - source.position;
        RaycastHit hit;
        if (Physics.Raycast(source.position, direction, out hit, 1f, playerLayer)) {
            StartCoroutine(AttackPlayer());
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
        attacking = false;
    }

    IEnumerator AttackPlayer()
    {
        attacking = true;

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDelay / attackSpd);
        RaycastHit hit;
        if (Physics.Raycast(source.position, direction, out hit, 1f, playerLayer)) {
            // Damage Player
            if (hit.collider.gameObject.TryGetComponent(out HealthBase player)) {
                player.TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(attackRecover / attackSpd);
        
        attacking = false;
    }

    public bool Attacking()
    {
        return attacking;
    }
}
