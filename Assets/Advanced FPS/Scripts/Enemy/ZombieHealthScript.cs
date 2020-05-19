using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthScript : HealthBase
{
    public int initialHealth = 10;
    private Animator animator;
    private Collider col;

    void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();
    }
    void Start()
    {
        health = initialHealth;
    }

    public override void Die()
    {
        animator.SetTrigger("Dead");
        col.isTrigger = true;
        Destroy(gameObject, 3);

        MonoBehaviour[] zombieScripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in zombieScripts) {
            script.enabled = false;
        }
    }




}
