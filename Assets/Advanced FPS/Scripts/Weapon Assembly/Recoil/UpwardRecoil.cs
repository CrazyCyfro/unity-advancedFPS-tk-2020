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
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();

        playerCamera.transform.Rotate(-maxVerticalAngle, Random.Range(-maxHorizontalAngle, maxHorizontalAngle), 0);
    }

    public override void RecoilScoped()
    {
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();

        playerCamera.transform.Rotate(-maxVerticalAngle/scopedRecoilBonus, Random.Range(-maxHorizontalAngle, maxHorizontalAngle)/scopedRecoilBonus, 0);
        
    }

    public override float RecoverySpeed()
    {
        return recoverySpeed;
    }
}
