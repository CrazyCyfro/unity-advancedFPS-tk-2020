using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthScript : HealthBase
{
    public int initialHealth = 10;
    private Animator animator;
    private Collider col;

    private EntityLootScript loot;

    void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();

        loot = GetComponentInChildren<EntityLootScript>();
    }
    void Start()
    {
        health = initialHealth;
    }

    public override void Die()
    {
        animator.SetTrigger("Dead");
        col.enabled = false;
        

        if (loot != null) loot.DropLoot();

        MonoBehaviour[] zombieScripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in zombieScripts) {
            script.enabled = false;
        }
        Destroy(gameObject, 3);
    }




}
