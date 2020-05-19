using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsPlayerHealthScript : HealthBase
{
    public int initialHealth;

    void Start()
    {
        health = initialHealth;
    }

    public override void Die()
    {
        Debug.Log("Player died");
    }
}
