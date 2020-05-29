using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFireMech : FireMechanism
{
    public int damage;
    public float range;
    private Camera playerCamera;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        animator.SetFloat("Hip Fire Speed", 1/cooldown);
    }
    public override void Fire()
    {
        
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();
        
        if (animator != null) animator.SetTrigger("Hip Fire");

        // Check for impact. If present, continue.
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range)) return;
        
        HealthBase enemy = hit.collider.GetComponentInParent<HealthBase>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
        }

        IBleedable target = hit.collider.GetComponentInParent<IBleedable>();
        if (target != null) {
            target.Bleed(hit.point, hit.normal);
        }
    }
}
