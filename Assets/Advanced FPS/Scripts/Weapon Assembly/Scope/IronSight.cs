using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSight : ScopeBase, IScopeStatus
{
    public float adsTime;
    private IReloadStatus reloadStatus;
    private Animator animator;
    private bool scoped;

    void Awake()
    {
        reloadStatus = GetComponent<IReloadStatus>();
        animator = GetComponent<Animator>();
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

        // transform.position = playerCamera.transform.position;
        // scoped = true;
    }

    private void Relax()
    {
        animator.SetBool("Scoped", false);
        scoped = false;
        // transform.localPosition = Vector3.zero;
        // scoped = false;
    }

    // private void RelaxInstant()
    // {
    //     animator.Rebind();
    //     scoped = false;
    // }

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
}
