using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDespawnScript : MonoBehaviour
{
    public float maxLifetime;
    private float lifetime;

    void Awake()
    {
        lifetime = 0;
    }
    void Update()
    {
        if (transform.parent.parent == null) {
            lifetime += Time.deltaTime;
        } else {
            lifetime = 0;
        }

        if (lifetime > maxLifetime) {
            Destroy(transform.root.gameObject);
        }
    }
}
