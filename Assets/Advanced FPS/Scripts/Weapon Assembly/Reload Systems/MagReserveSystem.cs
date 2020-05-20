﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagReserveSystem : ReloadSystem, IAddReset, IReloadStatus
{
    public int reserve;
    public int magSize;
    public float reloadDuration;
    private Animator animator;
    private int mag;
    private int initReserve;
    private bool reloading =  false;
    private IEnumerator reloadCoroutine;

    void Awake()
    {
        initReserve = reserve;
        mag = magSize;

        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        animator.SetFloat("Reload Speed", 1/reloadDuration);

        if (reserve > 0 && mag == 0) Reload();
        
    }

    void OnDisable()
    {
        if (reloading) CancelReload();
    }
    public override bool CanFire()
    {
        if (mag == 0) return false;
        if (reloading) return false;

        return true;
    }

    public override void Fired()
    {
        mag -= 1;
        

        // Debug.Log("Mag: " + mag);
        // Debug.Log("Reserve: " + reserve);
        
    }

    public override void PostFireAction()
    {
        if (reserve > 0 && mag == 0) Reload();
    }

    public override void Reload()
    {
        if (reloading) return;
        if (mag == magSize) return;
        if (reserve == 0) return;

        reloadCoroutine = ReloadTimer();
        StartCoroutine(reloadCoroutine);
        
        animator.SetTrigger("Reload");

        // Debug.Log("Reload started");
    }

    public override void CancelReload()
    {
        if (reloadCoroutine == null) return;
        StopCoroutine(reloadCoroutine);
        // if (reloading) Debug.Log("Reload cancelled");
        reloading = false;
        
    }


    IEnumerator ReloadTimer()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadDuration);

        int deficit = magSize - mag;
        if (reserve >= deficit) {
            reserve -= deficit;
            mag += deficit;
        } else {
            mag += reserve;
            reserve = 0;
        }
        // Debug.Log("Reload finished");
        reloading = false;

        FpsEvents.UpdateWeaponData.Invoke();
        FpsEvents.UpdateHudEvent.Invoke();
    }

    public int Add(int a)
    {
        reserve += a;
        return 0;
    }

    public void Reset()
    {
        reserve = initReserve;
    }

    public bool OutOfAmmo()
    {
        return (mag + reserve == 0);
    }

    public bool Reloading()
    {
        return reloading;
    }

    public string AmmoString()
    {
        return mag.ToString() + "/" + reserve.ToString();
    }
}
