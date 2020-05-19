using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSystem : ReloadSystem, IReloadStatus, ITransferable
{
    public int capacity;
    public int initAmmo;
    public AmmoType type;
    private int ammo;

    void Awake()
    {
        if (initAmmo > capacity) Debug.LogError("Initial ammo > capacity");
        ammo = initAmmo;
    }
    public int Add(int a)
    {
        int spare = capacity - ammo;

        if (spare < 0) Debug.Log("Capcity > Ammo");

        if (spare >= a) {
            ammo += a;
            Debug.Log("Adding " + a);
        } else {
            ammo += spare;
            Debug.Log("Adding " + spare);
        }

        return a - spare;
    }

    public string AmmoString()
    {
        return ammo.ToString();
    }

    public override void CancelReload(){}

    public override bool CanFire()
    {
        return true;
    }

    public override void Fired()
    {
        ammo--;

        if (ammo <= 0) {
            var switcher = GetComponentInParent<FpsWeaponSwitchScript>();
            switcher.AssignActiveWeapon();
            Destroy(gameObject);
        }
    }

    public bool OutOfAmmo()
    {
        return false;
    }

    public override void Reload(){}

    public bool Reloading()
    {
        return false;
    }

    public void Reset()
    {
        ammo = capacity;
    }

    public AmmoType Type()
    {
        return type;
    }

    public void Transfer(ITransferable target)
    {
        if (Type() == target.Type()) {
            int excess = target.Add(ammo);
            Debug.Log("Excess: " + excess);

            if (excess <= 0) {
                Destroy(gameObject);
                Debug.Log("Excess <= 0, destroying");
            } else {
                ammo = excess;
            }
        }
    }
}
