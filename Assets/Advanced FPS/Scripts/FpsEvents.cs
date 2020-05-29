using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class FpsEvents
{
    public static UnityEvent UpdateWeaponData = new UnityEvent();
    public static UnityEvent UpdateHudEvent = new UnityEvent();
    public static UnityEvent UpdateHeldWeapon = new UnityEvent();

    public static UnityEvent PlayerDeadEvent = new UnityEvent();

    public class RecoilVectorEvent : UnityEvent<RecoilData>{};
    public static RecoilVectorEvent RecoilEvent = new RecoilVectorEvent();
}

public class RecoilData
{
    public Vector3 recoilVector;
    public float time;
    public RecoilData(Vector3 recoilVector, float time) {
        this.recoilVector = recoilVector;
        this.time = time;
    }
}
