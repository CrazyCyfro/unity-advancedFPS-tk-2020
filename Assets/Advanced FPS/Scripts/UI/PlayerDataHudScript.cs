using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDataHudScript : MonoBehaviour
{
    public PlayerData playerData;

    public TextMeshProUGUI healthDisplay;

    void Awake()
    {
        FpsEvents.UpdateHudEvent.AddListener(UpdateHealth);
    }

    void UpdateHealth()
    {
        healthDisplay.text = playerData.playerHealth.ToString();
    }
}
