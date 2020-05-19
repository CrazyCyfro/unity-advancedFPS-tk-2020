using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeFireMech : FireMechanism
{
    public float force;
    public GameObject throwablePrefab;
    private Camera playerCamera;
    public override void Fire()
    {
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();

        GameObject nade = Instantiate(throwablePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);
        Rigidbody nadeRb = nade.GetComponent<Rigidbody>();

        nadeRb.AddForce(force * playerCamera.transform.forward);
    }
}
