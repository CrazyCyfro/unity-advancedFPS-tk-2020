using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmInputScript : MonoBehaviour
{
    public KeyInput fireKey;
    public KeyInput reloadKey;
    public KeyInput scopeKey;
    
    private FireMechanism fireMech;
    private ReloadSystem reloadSys;
    private RecoilBase recoil;
    private IScopeStatus scopeStatus;
    private ScopeBase scope;
    void Awake()
    {
        fireMech = GetComponent<FireMechanism>();
        reloadSys = GetComponent<ReloadSystem>();
        recoil = GetComponent<RecoilBase>();
        scopeStatus = GetComponent<IScopeStatus>();
        scope = GetComponent<ScopeBase>();
    }

    void Update()
    {
        if (fireKey.KeyActive()) {
            if (fireMech != null  && reloadSys != null) {
                FireWeapon();
                RefreshHud();
            }
        }

        if (reloadKey.KeyActive()) {
            if (reloadSys != null) {
                reloadSys.Reload();
                RefreshHud();
            }
        }

        if (scopeKey.KeyActive()) {
            if (scope != null) {
                scope.Scope();
                RefreshHud();
            }
            
        }
    }

    void FireWeapon()
    {
        if (fireMech.CooledDown() && reloadSys.CanFire()) {
            fireMech.Fire();
            reloadSys.Fired();
            StopAllCoroutines();
            StartCoroutine(FiredDelay());

            if (scopeStatus.Scoped()) {
                recoil.RecoilScoped();
            } else {
                recoil.Recoil();
            }
        }
    }

    void RefreshHud()
    {
        FpsEvents.UpdateWeaponData.Invoke();
        FpsEvents.UpdateHudEvent.Invoke();
    }

    IEnumerator FiredDelay()
    {
        yield return new WaitForSeconds(fireMech.cooldown);
        if (reloadSys != null) {
            reloadSys.PostFireAction();
            RefreshHud();
        }
    }
}
