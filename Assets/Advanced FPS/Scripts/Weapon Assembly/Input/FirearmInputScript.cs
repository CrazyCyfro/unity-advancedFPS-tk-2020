using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmInputScript : MonoBehaviour
{
    public KeyInput fireKey;
    public KeyInput reloadKey;
    public KeyInput scopeKey;
    
    private FireMechanism fireMech;
    private AmmoSystem ammoSys;
    private IReloadable reloadable;
    private RecoilBase recoil;
    private IScopeStatus scopeStatus;
    private ScopeBase scope;
    void Awake()
    {
        fireMech = GetComponent<FireMechanism>();
        ammoSys = GetComponent<AmmoSystem>();
        recoil = GetComponent<RecoilBase>();
        scopeStatus = GetComponent<IScopeStatus>();
        scope = GetComponent<ScopeBase>();
        reloadable = GetComponent<IReloadable>();
    }

    void Update()
    {

        if (fireKey.KeyActive()) {
            if (fireMech != null  && ammoSys != null) {
                FireWeapon();
                FpsEvents.FpsUpdateHud();
            }
        }

        if (reloadKey.KeyActive()) {
            if (reloadable != null) {
                reloadable.Reload();
                FpsEvents.FpsUpdateHud();
            }
        }

        if (scopeKey.KeyActive()) {
            if (scope != null) {
                scope.Scope();
                FpsEvents.FpsUpdateHud();
            }
            
        }
    }

    void FireWeapon()
    {
        if (fireMech.CooledDown() && ammoSys.CanFire()) {
            fireMech.Fire();
            ammoSys.Fired();

            if (scopeStatus.Scoped()) {
                recoil.RecoilScoped();
            } else {
                recoil.Recoil();
            }
        }
    }
}
