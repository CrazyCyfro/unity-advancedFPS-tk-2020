using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpwardRecoil : RecoilBase
{
    public float recoveryTime;
    public float maxVerticalAngle;
    public float maxHorizontalAngle;

    public float scopedRecoilBonus;
    private Camera playerCamera;
    public override void Recoil()
    {

        FpsEvents.RecoilEvent.Invoke(
            new RecoilData(
                new Vector3(-maxVerticalAngle, Random.Range(-maxHorizontalAngle, maxHorizontalAngle), 0), 
                recoveryTime));

        // if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();
        // playerCamera.transform.Rotate(-maxVerticalAngle, Random.Range(-maxHorizontalAngle, maxHorizontalAngle), 0);
    }

    public override void RecoilScoped()
    {

        FpsEvents.RecoilEvent.Invoke(
            new RecoilData(
                new Vector3(-maxVerticalAngle/scopedRecoilBonus, Random.Range(-maxHorizontalAngle, maxHorizontalAngle)/scopedRecoilBonus, 0),
                recoveryTime/scopedRecoilBonus));

        // if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();
        // playerCamera.transform.Rotate(-maxVerticalAngle/scopedRecoilBonus, Random.Range(-maxHorizontalAngle, maxHorizontalAngle)/scopedRecoilBonus, 0);
        
    }
}
