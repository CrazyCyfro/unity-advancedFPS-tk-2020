using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHudScript : MonoBehaviour
{
    public Canvas canvas;
    public SceneManagerScript sceneManager;
    void Awake()
    {
        FpsEvents.PlayerDeadEvent.AddListener(PlayerDeath);
    }

    void PlayerDeath()
    {
        canvas.enabled = true;

        StartCoroutine(DelayLoadMainMenu());
    }

    IEnumerator DelayLoadMainMenu()
    {
        yield return new WaitForSeconds(5);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        sceneManager.LoadScene("Main Menu");
    }
}
