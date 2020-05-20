using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperFireMech : FireMechanism
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
        
        // Check for impact. If present, continue. Inaccurate when not scoped in
        RaycastHit hit;
        if (scope.Scoped()) {
            if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity)) return;
        } else {
            animator.SetTrigger("Hip Fire");
            Vector3 randomPos = new Vector3(Random.Range(0, playerCamera.pixelWidth), Random.Range(0, playerCamera.pixelHeight), 0);
            Ray ray = playerCamera.ScreenPointToRay(randomPos);
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity)) return;
        }

        if (hit.collider.gameObject.TryGetComponent(out HealthBase enemy)) {
            
            enemy.TakeDamage(damage);
        }


        // Spawn impact particles, destroy after animation is over
        // GameObject particles = Instantiate(bulletImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        // Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
    }
}
