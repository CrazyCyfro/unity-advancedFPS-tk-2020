using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponInputScript : MonoBehaviour
{
    public KeyInput fireKey;
        
    private FireMechanism fireMech;
    void Awake()
    {
        fireMech = GetComponent<FireMechanism>();
    }

    void Update()
    {
        if (fireKey.KeyActive()) {
            if (fireMech != null) {
                if (fireMech.CooledDown()) {
                    fireMech.Fire();
                    RefreshHud();
                }
            }
        }
    }

    void RefreshHud()
    {
        FpsEvents.UpdateWeaponData.Invoke();
        FpsEvents.UpdateHudEvent.Invoke();
    }
}
