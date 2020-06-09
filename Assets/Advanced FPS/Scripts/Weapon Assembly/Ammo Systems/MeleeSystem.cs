using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : AmmoSystem, IReloadStatus
{

    public override bool CanFire()
    {
        return true;
    }

    public override void Fired()
    {
        
    }
    public string AmmoString()
    {
        return string.Empty;
    }

    public bool OutOfAmmo()
    {
        return false;
    }
    public bool Reloading()
    {
        return false;
    }
}
