using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsAdsScript : MonoBehaviour
{
    private ScopeBase scope;
    
    void Awake()
    {
        FpsEvents.UpdateHeldWeapon.AddListener(AssignHeldWeapon);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Scope();
        }
    }

    private void Scope()
    {
        if (scope == null) return;
        scope.Scope();
        FpsEvents.UpdateWeaponData.Invoke();
        FpsEvents.UpdateHudEvent.Invoke();
    }
    
    public void AssignHeldWeapon()
    {
        scope = GetComponentInChildren<ScopeBase>();
    }
}
