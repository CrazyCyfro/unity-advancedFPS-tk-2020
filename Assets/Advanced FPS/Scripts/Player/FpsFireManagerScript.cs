using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsFireManagerScript : MonoBehaviour
{
    public Transform weaponPos;
    private FireMechanism fireMech;
    private ReloadSystem reloadSys;
    private RecoilBase recoil;
    private IScopeStatus scope;

    void Awake()
    {
        FpsEvents.UpdateHeldWeapon.AddListener(AssignHeldWeapon);
    }

    void Update()
    {
        if (weaponPos.childCount == 0) return;
        if (fireMech == null || reloadSys == null) return;

        if (fireMech.mode == FireMechanism.FireMode.Semi) {
            if (Input.GetButtonDown("Fire1")) {
                FireWeapon();
            }
        } else if (fireMech.mode == FireMechanism.FireMode.Auto) {
            if (Input.GetButton("Fire1")) {
                FireWeapon();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            reloadSys.Reload();
            RefreshHud();
        }
    }

    public void AssignHeldWeapon()
    {
        fireMech = weaponPos.GetComponentInChildren<FireMechanism>();
        reloadSys = weaponPos.GetComponentInChildren<ReloadSystem>();
        recoil = weaponPos.GetComponentInChildren<RecoilBase>();
        scope = weaponPos.GetComponentInChildren<IScopeStatus>();
    }

    void FireWeapon()
    {
        if (fireMech.CooledDown() && reloadSys.CanFire()) {
            fireMech.Fire();
            reloadSys.Fired();
            StartCoroutine(FiredDelay());

            if (scope.Scoped()) {
                recoil.RecoilScoped();
            } else {
                recoil.Recoil();
            }

            RefreshHud();
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
        reloadSys.PostFireAction();
        RefreshHud();
    }
}
