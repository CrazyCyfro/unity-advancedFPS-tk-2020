using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBleedScript : MonoBehaviour, IBleedable
{
    public GameObject bloodPrefab;
    public void Bleed(Vector3 point, Vector3 normal)
    {
        GameObject blood = Instantiate(bloodPrefab, point, Quaternion.LookRotation(normal));
        var system = blood.GetComponentInChildren<ParticleSystem>();
        Destroy(blood, system.main.duration);
    }
}
