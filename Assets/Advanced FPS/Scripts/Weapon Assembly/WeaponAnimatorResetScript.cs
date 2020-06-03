using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorResetScript : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        // animator.keepAnimatorControllerStateOnDisable = false;
    }

    void OnDisable()
    {
        animator.Rebind();
        // animator.Play("Reset", 0, 0f);
    }
}
