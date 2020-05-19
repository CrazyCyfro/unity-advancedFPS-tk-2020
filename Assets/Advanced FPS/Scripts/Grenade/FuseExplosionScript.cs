using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseExplosionScript : MonoBehaviour
{
    public int damage;
    public float fuse;
    public float radius;

    public float power;
    private float lift = 10.0f;

    void Start()
    {
        StartCoroutine(FuseExplode());
    }
    IEnumerator FuseExplode()
    {
        yield return new WaitForSeconds(fuse);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in colliders)
        {
            Rigidbody rb = c.GetComponentInParent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(power, transform.position, radius, lift);
            }

            HealthBase health = c.GetComponentInParent<HealthBase>();
            if (health != null) {
                health.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
