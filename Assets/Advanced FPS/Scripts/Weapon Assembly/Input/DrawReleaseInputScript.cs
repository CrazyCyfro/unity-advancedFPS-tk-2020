using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawReleaseInputScript : MonoBehaviour
{
    public KeyInput drawKey;
    public KeyInput releaseKey;

    public KeyInput reloadKey;
        
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
            Debug.Log("Drawing");
            if (drawRelease != null) {
                if (fireMech.PeekCooled()) drawRelease.Draw();
            }   
        }

        if (releaseKey.KeyActive()) {
            if (drawRelease.Drawn()) {
                if (fireMech != null  && reloadSys != null && drawRelease != null) {
                    FireWeapon();
                    FpsEvents.FpsUpdateHud();
                }
            }
            drawRelease.Release(); 
        }

        if (reloadKey.KeyActive()) {
            if (reloadSys != null) {
                reloadSys.Reload();
                FpsEvents.FpsUpdateHud();
            }
        }
    }

    void FireWeapon()
    {
        if (fireMech.CooledDown() && reloadSys.CanFire()) {
            fireMech.Fire();
            reloadSys.Fired();
        }
    }
}
