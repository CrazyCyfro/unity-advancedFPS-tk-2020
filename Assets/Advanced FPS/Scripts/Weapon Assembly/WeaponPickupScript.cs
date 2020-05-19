using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupScript : MonoBehaviour
{
    public int[] slots;
    private float thrust;
    private const int FP_LAYER = 10;
    private const int WEAPON_LAYER = 11;
    Animator animator;
    Rigidbody rb;
    Collider col;
    Transform[] children;
    MonoBehaviour[] components;
    void Start()
    {
        thrust = 10;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        col = GetComponentInChildren<MeshCollider>();

        components = GetComponents<MonoBehaviour>();
        children = GetComponentsInChildren<Transform>();
    }
    public void Pickup()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        foreach(Transform child in children)
        {
            child.gameObject.layer = FP_LAYER;
        }
        
        foreach (MonoBehaviour script in components)
        {
            script.enabled = true;
        }

        rb.isKinematic = true;
        col.enabled = false;
        if (animator != null) animator.enabled = true;
    }

    public void Drop(Transform dropOffPoint)
    {

        foreach(Transform child in children)
        {
            child.gameObject.layer = WEAPON_LAYER;
        }
        
        foreach (MonoBehaviour script in components)
        {
            script.enabled = false;
        }

        transform.SetParent(null);
        transform.localScale = Vector3.one;
        transform.position = dropOffPoint.position;

        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        col.enabled = true;
        
        if (animator != null) animator.enabled = false;

        rb.AddForce(Vector3.up * thrust);
    }
}
