using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnsheatheScript : MonoBehaviour
{
    public float unsheatheDuration;
    public MonoBehaviour[] delayActivation;
    public MonoBehaviour[] immediateActivation;
    
    private Animator animator;
    private WeaponHudScript hudScript;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hudScript = GetComponent<WeaponHudScript>();
    }
    void OnEnable()
    {
        animator.SetFloat("Unsheathe Speed", 1/unsheatheDuration);

        foreach (MonoBehaviour script in immediateActivation)
        {
            script.enabled = true;
        }

        FpsEvents.FpsUpdateHud();

        StartCoroutine(UnsheatheWeapon());
    }

    void OnDisable()
    {
        StopAllCoroutines();

        foreach (MonoBehaviour script in delayActivation)
        {
            script.enabled = false;
        }
    }

    IEnumerator UnsheatheWeapon()
    {
        yield return new WaitForSeconds(unsheatheDuration);

        foreach (MonoBehaviour script in delayActivation)
        {
            script.enabled = true;
        }
    }
}
