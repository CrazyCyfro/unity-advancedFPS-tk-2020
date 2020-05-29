using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsRecoilRecoveryScript : MonoBehaviour
{
    public Transform trueDirection;
    private Camera playerCamera;
    private RecoilBase recoil;
    private Quaternion startRotation;
    private float recoilTime;
    private float t;

    void Awake()
    {
        t = 0;
        startRotation = trueDirection.rotation;
        playerCamera = GetComponentInChildren<Camera>();

        FpsEvents.UpdateHeldWeapon.AddListener(AssignHeldWeapon);
        FpsEvents.RecoilEvent.AddListener(RecoilCamera);
    }

    void Update()
    {
        if (recoil != null) UpdateCameraRotation();
    }
    void UpdateCameraRotation()
    {
        t += Time.deltaTime/recoilTime;
        playerCamera.transform.rotation = Quaternion.Slerp(startRotation, trueDirection.rotation, t);
    }

    void AssignHeldWeapon()

    {
        recoil = transform.GetComponentInChildren<RecoilBase>();
    }


    void RecoilCamera(RecoilData data)
    {
        playerCamera.transform.Rotate(data.recoilVector);
        startRotation = playerCamera.transform.rotation;

        t = 0;
        recoilTime = data.time;
    }
}
