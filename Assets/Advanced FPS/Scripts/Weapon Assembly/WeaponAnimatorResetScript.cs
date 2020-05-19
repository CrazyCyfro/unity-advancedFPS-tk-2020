﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorResetScript : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnDisable()
    {
        animator.Rebind();
    }
}
