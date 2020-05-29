using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeFireMech : FireMechanism
{
    public float force;
    public GameObject throwablePrefab;
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

        animator.SetTrigger("Hip Fire");

        GameObject nade = Instantiate(throwablePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);
        Rigidbody nadeRb = nade.GetComponent<Rigidbody>();

        nadeRb.AddForce(force * playerCamera.transform.forward);
    }
}
