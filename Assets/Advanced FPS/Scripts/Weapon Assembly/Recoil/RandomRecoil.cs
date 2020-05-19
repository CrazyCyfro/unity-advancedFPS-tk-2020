using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRecoil : RecoilBase
{
    public float recoverySpeed;
    public float maxAngle;
    public float scopedRecoilBonus;
    private Camera playerCamera;
    public override void Recoil()
    {
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();

        playerCamera.transform.Rotate(Random.Range(-maxAngle, maxAngle), Random.Range(-maxAngle, maxAngle), 0);
    }

    public override void RecoilScoped()
    {
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();

        playerCamera.transform.Rotate(Random.Range(-maxAngle, maxAngle)/scopedRecoilBonus, Random.Range(-maxAngle, maxAngle)/scopedRecoilBonus, 0);
    }

    public override float RecoverySpeed()
    {
        return recoverySpeed;
    }
}
