using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireMechanism : MonoBehaviour
{
    public float cooldown;
    private float t = 0;
    public bool CooledDown()
    {
        if (Time.time - t > cooldown)
        {
            t = Time.time;
            return true;
        } else {
            return false;
        }
    }

    public bool PeekCooled()
    {
        if (Time.time - t > cooldown)
        {
            return true;
        } else {
            return false;
        }
    }

    public abstract void Fire();
}
