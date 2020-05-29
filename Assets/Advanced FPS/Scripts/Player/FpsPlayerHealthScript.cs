using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsPlayerHealthScript : HealthBase
{
    public int initialHealth;
    public PlayerData playerData;

    private MonoBehaviour[] components;
    private Rigidbody rb;

    void Awake()
    {
        components = GetComponents<MonoBehaviour>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        health = initialHealth;

        playerData.playerHealth = health;

        FpsEvents.UpdateHudEvent.Invoke();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        playerData.playerHealth = health;

        FpsEvents.UpdateHudEvent.Invoke();
    }

    public override void Die()
    {
        FpsEvents.PlayerDeadEvent.Invoke();

        Debug.Log("Player died");

        rb.isKinematic = true;

        foreach (MonoBehaviour component in components) {
            component.enabled = false;
        }
    }
}
