using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : ReloadSystem, IReloadStatus
{

    public override void CancelReload()
    {
        
    }

    public override bool CanFire()
    {
        return true;
    }

    public override void Fired()
    {
        
    }
    public override void Reload()
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
