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
    public override void Fire()
    {
        
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();
        
        if (!scope.Scoped()) animator.SetTrigger("Hip Fire");

        // Check for impact. If present, continue.
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity)) return;
        
        if (hit.collider.gameObject.TryGetComponent(out HealthBase enemy)) {
            
            enemy.TakeDamage(damage);
        }


        // Spawn impact particles, destroy after animation is over
        // GameObject particles = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        // Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
    }
}
