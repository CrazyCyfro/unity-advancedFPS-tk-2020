using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFireMech : FireMechanism
{
    public int damage;
    private Camera playerCamera;
    private IScopeStatus scope;
    private Animator animator;

    void Awake()
    {
        scope = GetComponent<IScopeStatus>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        animator.SetFloat("Hip Fire Speed", 1/cooldown);
    }
    public override void Fire()
    {
        
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();
        
        if (!scope.Scoped()) {
            animator.SetTrigger("Hip Fire");
        }

        // Check for impact. If present, continue.
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity)) return;
        
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
