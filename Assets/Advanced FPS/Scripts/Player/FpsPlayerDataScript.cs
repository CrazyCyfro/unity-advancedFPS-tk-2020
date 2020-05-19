using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsPlayerDataScript : MonoBehaviour
{
    public PlayerData playerData;

    void Update()
    {
        playerData.playerPos = transform.position;
    }
}
