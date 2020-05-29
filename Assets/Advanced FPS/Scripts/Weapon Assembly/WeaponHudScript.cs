using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHudScript : MonoBehaviour
{
    private IReloadStatus reloadStatus;
    private IScopeStatus scopeStatus;
    public ActiveWeaponData weaponData;

    void Awake()
    {
        reloadStatus = GetComponent<IReloadStatus>();
        scopeStatus = GetComponent<IScopeStatus>();
    }

    void OnDisable()
    {
        ClearData();
        FpsEvents.UpdateWeaponData.RemoveListener(UpdateData);
    }

    void OnEnable()
    {
        FpsEvents.UpdateWeaponData.AddListener(UpdateData);
    }

    void UpdateData()
    {
        weaponData.ammoString = reloadStatus.AmmoString();
        weaponData.outOfAmmo = reloadStatus.OutOfAmmo();
        weaponData.reloading = reloadStatus.Reloading();
        weaponData.scoping = scopeStatus.Scoped();
        weaponData.noCrossshair = scopeStatus.NoCrosshair();
    }

    void ClearData()
    {
        weaponData.ammoString = string.Empty;
        weaponData.outOfAmmo = false;
        weaponData.reloading = false;
        weaponData.scoping = false;
        weaponData.noCrossshair = false;
    }
}
