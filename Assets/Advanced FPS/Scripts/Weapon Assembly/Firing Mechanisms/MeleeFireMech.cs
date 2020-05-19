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
    public override void Fire()
    {
        
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();
        
        if (animator != null) animator.SetTrigger("Hip Fire");

        // Check for impact. If present, continue.
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range)) return;
        
        if (hit.collider.gameObject.TryGetComponent(out HealthBase enemy)) {
            
            enemy.TakeDamage(damage);
        }


        // Spawn impact particles, destroy after animation is over
        // GameObject particles = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        // Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
    }
}
