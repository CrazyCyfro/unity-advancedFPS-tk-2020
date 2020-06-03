using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpwardRecoil : RecoilBase
{
    public float recoverySpeed;
    public float maxVerticalAngle;
    public float maxHorizontalAngle;

    public float scopedRecoilBonus;
    private Camera playerCamera;
    public override void Recoil()
    {

        FpsEvents.RecoilEvent.Invoke(
            new RecoilData(
                new Vector3(-maxVerticalAngle, Random.Range(-maxHorizontalAngle, maxHorizontalAngle), 0), 
                recoverySpeed));
    }

    public override void RecoilScoped()
    {

        FpsEvents.RecoilEvent.Invoke(
            new RecoilData(
                new Vector3(-maxVerticalAngle/scopedRecoilBonus, Random.Range(-maxHorizontalAngle, maxHorizontalAngle)/scopedRecoilBonus, 0),
                recoverySpeed/scopedRecoilBonus));
        
    }
}
