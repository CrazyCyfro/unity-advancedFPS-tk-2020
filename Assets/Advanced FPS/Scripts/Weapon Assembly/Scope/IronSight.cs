using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSight : ScopeBase, IScopeStatus
{
    public float adsDelay;
    private IReloadStatus reloadStatus;
    private Animator animator;
    private bool scoped;

    void Awake()
    {
        reloadStatus = GetComponent<IReloadStatus>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        animator.SetFloat("Scope Speed", 1/adsDelay);
    }
    void OnDisable()
    {
        Relax();
    }
    
    void Update()
    {
        if (reloadStatus.Reloading())
        {
            Relax();
        } 
    }
    private void Aim()
    {

        animator.SetBool("Scoped", true);
        scoped = true;
    }

    private void Relax()
    {
        animator.SetBool("Scoped", false);
        scoped = false;
    }

    public override void Scope()
    {
        if (scoped) {
            Relax();
        } else {
            if (reloadStatus.Reloading()) return;
            Aim();
        }
    }

    public bool Scoped()
    {
        return scoped;
    }

    public bool NoCrosshair()
    {
        return false;
    }
}
