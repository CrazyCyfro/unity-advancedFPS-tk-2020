using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsRecoilRecoveryScript : MonoBehaviour
{
    public Transform trueDirection;
    private Camera playerCamera;
    private float recoverySpeed;

    void Awake()
    {
        recoverySpeed = 0;
        playerCamera = GetComponentInChildren<Camera>();

        FpsEvents.RecoilEvent.AddListener(RecoilCamera);
    }

    void Update()
    {
        UpdateCameraRotation();
    }
    void UpdateCameraRotation()
    {
        // t += Time.deltaTime/recoilTime;
        // playerCamera.transform.rotation = Quaternion.Slerp(startRotation, trueDirection.rotation, t);
        
        float step = recoverySpeed * Time.deltaTime;

        playerCamera.transform.rotation = Quaternion.RotateTowards(playerCamera.transform.rotation, trueDirection.rotation, step);
    }


    void RecoilCamera(RecoilData data)
    {
        playerCamera.transform.Rotate(data.recoilVector);

        recoverySpeed = data.speed;
    }
}
