using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsRecoilRecoveryScript : MonoBehaviour
{
    public Transform weaponPos;
    public Transform trueDirection;
    public Camera playerCamera;

    private RecoilBase recoil;

    void Awake()
    {
        FpsEvents.UpdateHeldWeapon.AddListener(AssignHeldWeapon);
    }

    void Update()
    {
        if (recoil != null) UpdateCameraRotation();
    }

    void AssignHeldWeapon()
    {
        recoil = weaponPos.GetComponentInChildren<RecoilBase>();
    }

    void UpdateCameraRotation()
    {
        if (playerCamera.transform.rotation.Equals(trueDirection.rotation)) return;

        playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, trueDirection.rotation, Time.deltaTime * recoil.RecoverySpeed());
    }
}
