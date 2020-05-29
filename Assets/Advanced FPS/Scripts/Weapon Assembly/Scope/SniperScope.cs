using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScope : ScopeBase, IScopeStatus
{
    public float zoomDelay;
    public float zoomedFOV;
    private float defaultFOV;
    private IReloadStatus reloadStatus;
    private Animator animator;
    private Camera playerCamera;
    private bool scoped;

    void Awake()
    {
        reloadStatus = GetComponent<IReloadStatus>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        animator.SetFloat("Scope Speed", 1/zoomDelay);
        playerCamera = GetComponentInParent<Camera>();
        defaultFOV = playerCamera.fieldOfView;
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
        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        yield return new WaitForSeconds(zoomDelay);

        if (scoped) playerCamera.fieldOfView = zoomedFOV;
    }

    private void Relax()
    {
        animator.SetBool("Scoped", false);
        scoped = false;
        playerCamera.fieldOfView = defaultFOV;
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
        return true;
    }
}
