using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveWeaponHudScript : MonoBehaviour
{
    public ActiveWeaponData weaponData;
    public GameObject crosshair;
    public TextMeshProUGUI weaponStatus;
    public TextMeshProUGUI ammoDisplay;

    void Awake()
    {
        FpsEvents.UpdateHudEvent.AddListener(UpdateHud);
    }

    void Start()
    {
        
        weaponStatus.text = string.Empty;
        ammoDisplay.text = string.Empty;
    }

    void UpdateHud()
    {
        ammoDisplay.text = weaponData.ammoString;

        if (weaponData.outOfAmmo) {
            weaponStatus.text = "OUT OF AMMO";
        } else if (weaponData.reloading) {
            weaponStatus.text = "RELOADING";
        } else {
            weaponStatus.text = string.Empty;
        }

        if (weaponData.scoping || weaponData.noCrossshair || weaponData.outOfAmmo || weaponData.reloading) {
            crosshair.SetActive(false);
        } else {
            crosshair.SetActive(true);
        }
    }
}
