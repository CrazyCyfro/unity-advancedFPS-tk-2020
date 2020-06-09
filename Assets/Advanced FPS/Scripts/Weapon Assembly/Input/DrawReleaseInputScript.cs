using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawReleaseInputScript : MonoBehaviour
{
    public KeyInput drawKey;
    public KeyInput releaseKey;

    public KeyInput reloadKey;
        
    private FireMechanism fireMech;
    private AmmoSystem ammoSys;
    private IReloadable reloadable;
    private DrawReleaseScript drawRelease;
    void Awake()
    {
        fireMech = GetComponent<FireMechanism>();
        ammoSys = GetComponent<AmmoSystem>();
        drawRelease = GetComponent<DrawReleaseScript>();
        reloadable = GetComponent<IReloadable>();
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
                if (fireMech != null  && ammoSys != null && drawRelease != null) {
                    FireWeapon();
                    FpsEvents.FpsUpdateHud();
                }
            }
            drawRelease.Release(); 
        }

        if (reloadKey.KeyActive()) {
            if (reloadable != null) {
                reloadable.Reload();
                FpsEvents.FpsUpdateHud();
            }
        }
    }

    void FireWeapon()
    {
        if (fireMech.CooledDown() && ammoSys.CanFire()) {
            fireMech.Fire();
            ammoSys.Fired();
        }
    }
}
