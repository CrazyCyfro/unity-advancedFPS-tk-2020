using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawReleaseInputScript : MonoBehaviour
{
    public KeyInput drawKey;
    public KeyInput releaseKey;
        
    private FireMechanism fireMech;
    private ReloadSystem reloadSys;
    private DrawReleaseScript drawRelease;
    void Awake()
    {
        fireMech = GetComponent<FireMechanism>();
        reloadSys = GetComponent<ReloadSystem>();
        drawRelease = GetComponent<DrawReleaseScript>();
    }

    void Update()
    {
        if (drawKey.KeyActive()) {
            if (drawRelease != null) {
                if (fireMech.PeekCooled()) drawRelease.Draw();
            }   
        }

        if (releaseKey.KeyActive()) {
            if (drawRelease.Drawn()) {
                if (fireMech != null  && reloadSys != null && drawRelease != null) {
                    FireWeapon();
                    RefreshHud();
                }
            }
            drawRelease.Release(); 
        }

        
    }

    void FireWeapon()
    {
        if (fireMech.CooledDown() && reloadSys.CanFire()) {
            fireMech.Fire();
            reloadSys.Fired();
        }
    }

    void RefreshHud()
    {
        FpsEvents.UpdateWeaponData.Invoke();
        FpsEvents.UpdateHudEvent.Invoke();
    }
}
